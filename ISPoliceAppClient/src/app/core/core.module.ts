import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSliderModule } from '@angular/material/slider';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
// import {
//     PerfectScrollbarModule,
//     PERFECT_SCROLLBAR_CONFIG,
//     PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { FullscreenComponent } from './fullscreen/fullscreen.component';
// import { SearchBarComponent } from './search-bar/search-bar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { SidemenuItemComponent } from './sidemenu-item/sidemenu-item.component';
import { SidemenuComponent } from './sidemenu/sidemenu.component';
import { ToolbarNotificationComponent } from './toolbar-notification/toolbar-notification.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { UserMenuComponent } from './user-menu/user-menu.component';
// const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
//     suppressScrollX: true
// };

@NgModule({
  declarations: [
    // SearchBarComponent,
    SidemenuComponent,
    SidebarComponent,
    SidemenuItemComponent,
    ToolbarComponent,
    FullscreenComponent,
    UserMenuComponent,
    ToolbarNotificationComponent,
  ],

  imports: [
    CommonModule,
    MatListModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatChipsModule,
    RouterModule,
    // PerfectScrollbarModule,
    NgScrollbarModule,
    FlexLayoutModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatSidenavModule,
    MatTabsModule,
    MatSliderModule,
    MatProgressBarModule,
  ],

  exports: [
    SidemenuComponent,
    SidemenuItemComponent,
    ToolbarNotificationComponent,
    ToolbarComponent,
    // SearchBarComponent,
    FullscreenComponent,
    SidebarComponent,
    UserMenuComponent,
  ],

  providers: [
    // {
    //     provide: PERFECT_SCROLLBAR_CONFIG,
    //     useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    // }
  ],
})
export class CoreModule {}
