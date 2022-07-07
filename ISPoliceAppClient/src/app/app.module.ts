import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DateAdapter, MAT_DATE_LOCALE } from '@angular/material/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { AdminModule } from './admin/admin.module';
//import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { LazyLoadModule } from './lazy-load/lazy-load.module';
import { MaterialModule } from './material.module';
import { CustomHttpInterceptorService } from './utilities/custom-http-interceptor.service';
import { AppDateAdapter } from './shared/appdate-adapter';
import { Platform } from '@angular/cdk/platform';
// import { RichTextEditorAllModule } from '@syncfusion/ej2-angular-richtexteditor';
// import { GenericListComponent } from './utilities/generic-list/generic-list.component';

@NgModule({
  declarations: [AppComponent /* GenericListComponent */],
  imports: [
    BrowserModule,
    CommonModule,
    //AppRoutingModule,
    MaterialModule,
    BrowserAnimationsModule,
    LazyLoadModule,
    HttpClientModule,
    CoreModule,
    FormsModule,
    AdminModule,
    ReactiveFormsModule,
    SweetAlert2Module.forRoot(),
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
    {
      provide: DateAdapter,
      useClass: AppDateAdapter,
      deps: [MAT_DATE_LOCALE, Platform],
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CustomHttpInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
