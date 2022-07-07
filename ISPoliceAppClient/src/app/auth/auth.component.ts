import { Component, Input, OnInit } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { MatDrawerMode } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { SpinnerService } from '../utilities/spinner.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
})
export class AuthComponent implements OnInit {
  @Input() isVisible: boolean = true;
  visibility = 'shown';

  sideNavOpened: boolean = true;
  matDrawerOpened: boolean = false;
  matDrawerShow: boolean = true;
  sideNavMode: MatDrawerMode = 'side';

  ngOnChanges() {
    this.visibility = this.isVisible ? 'shown' : 'hidden';
  }

  constructor(
    private media: MediaObserver,
    public spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {}

  getRouteAnimation(outlet: RouterOutlet) {
    return outlet.activatedRouteData.animation;
    //return outlet.isActivated ? outlet.activatedRoute : ''
  }

  toggleView() {
    if (this.media.isActive('gt-md')) {
      this.sideNavMode = 'side';
      this.sideNavOpened = true;
      this.matDrawerOpened = false;
      this.matDrawerShow = true;
    } else if (this.media.isActive('gt-xs')) {
      this.sideNavMode = 'side';
      this.sideNavOpened = false;
      this.matDrawerOpened = true;
      this.matDrawerShow = true;
    } else if (this.media.isActive('lt-sm')) {
      this.sideNavMode = 'over';
      this.sideNavOpened = false;
      this.matDrawerOpened = false;
      this.matDrawerShow = false;
    }
  }
}
