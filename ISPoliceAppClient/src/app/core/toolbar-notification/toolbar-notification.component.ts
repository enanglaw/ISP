import { Component, ElementRef, Input, OnInit } from '@angular/core';
import { Notification } from '../toolbar/toolbar.helpers';

@Component({
  selector: 'app-toolbar-notification',
  templateUrl: './toolbar-notification.component.html',
  styleUrls: ['./toolbar-notification.component.scss'],
})
export class ToolbarNotificationComponent implements OnInit {
  cssPrefix = 'toolbar-notification';
  isOpen: boolean = false;
  @Input() notifications: Notification[] = [];

  // @HostListener('document:click', ['$event', '$event.target'])
  // onClick(event: MouseEvent, targetElement: HTMLElement) {
  //     if (!targetElement) {
  //           return;
  //     }
  //     const clickedInside = this.elementRef.nativeElement.contains(targetElement);
  //     if (!clickedInside) {
  //          this.isOpen = false;
  //     }
  // }

  constructor(private elementRef: ElementRef) {}

  ngOnInit() {}

  select() {}

  delete(notification: any) {}
}
