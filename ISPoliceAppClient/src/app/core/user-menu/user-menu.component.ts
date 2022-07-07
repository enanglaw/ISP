import {
  Component,
  ElementRef,
  HostListener,
  Input,
  OnInit,
} from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-menu',
  templateUrl: './user-menu.component.html',
  styleUrls: ['./user-menu.component.scss'],
})
export class UserMenuComponent implements OnInit {
  isOpen: boolean = false;

  //currentUser
  Hari: any;

  @Input() currentUser: any;
  @HostListener('document:click', ['$event', '$event.target'])
  onClick(event: MouseEvent, targetElement: HTMLElement) {
    if (targetElement != null) {
      if (targetElement.innerText!.toLowerCase().indexOf('logout') > -1) {
        this.router.navigate(['/login']);
      }
    }
    if (!targetElement) {
      return;
    }

    const clickedInside = this.elementRef.nativeElement.contains(targetElement);
    if (!clickedInside) {
      this.isOpen = false;
    }
  }

  constructor(private elementRef: ElementRef, private router: Router) {}

  ngOnInit() {}

  logout() {
    this.router.navigate(['/login']);
  }
}
