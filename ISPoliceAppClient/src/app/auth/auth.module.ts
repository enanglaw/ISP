import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { CoreModule } from '../core/core.module';
import { DashboardModule } from '../dashboard/dashboard.module';
import { AuthComponent } from './auth.component';
import { appRoutes } from './lazyloader.routes';

@NgModule({
  declarations: [AuthComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(appRoutes),
    MatToolbarModule,
    DashboardModule,
    MatButtonModule,
    MatIconModule,
    MatTabsModule,
    CoreModule,
    MatSidenavModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    // PerfectScrollbarModule,
    NgScrollbarModule,
    FlexLayoutModule
  ],
})
export class AuthModule {}
