import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { SpinnerService } from './spinner.service';

@Injectable({
  providedIn: 'root',
})
export class CustomHttpInterceptorService {
  constructor(private spinnerService: SpinnerService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.spinnerService.show();
    return next.handle(req).pipe(
      /* tap(
        (event: HttpEvent<any>) => {
          if (event instanceof HttpResponse) {
            this.spinnerService.hide();
          }
        },
        (error) => {
          this.spinnerService.hide();
        }
      ) */
      finalize(() => {
        this.spinnerService.hide();
      })
    );
  }
}
