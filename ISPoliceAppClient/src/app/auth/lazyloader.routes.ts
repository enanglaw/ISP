import { Routes } from '@angular/router';
import { CreateVenuePermissiontypeComponent } from '../admin/master/venue-permissiontype/create-venue-permissiontype/create-venue-permissiontype.component';
import { CreateVenueComponent } from '../admin/master/venue/create-venue/create-venue.component';
import { EditVenuePermissiontypeComponent } from '../admin/master/venue-permissiontype/edit-venue-permissiontype/edit-venue-permissiontype.component';
import { EditVenueComponent } from '../admin/master/venue/edit-venue/edit-venue.component';
import { IndexVenuePermissiontypeComponent } from '../admin/master/venue-permissiontype/index-venue-permissiontype/index-venue-permissiontype.component';
import { IndexVenueComponent } from '../admin/master/venue/index-venue/index-venue.component';
import { CreateWorkflowComponent } from '../admin/workflow/create-workflow/create-workflow.component';
import { EditWorkflowComponent } from '../admin/workflow/edit-workflow/edit-workflow.component';
import { IndexWorkflowComponent } from '../admin/workflow/index-workflow/index-workflow.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { AuthComponent } from './auth.component';
import { ControlRoomEditComponent } from '../admin/control-room/control-room-edit/control-room-edit.component';
import { IndexPersonComponent } from '../pages/person/index-person/index-person.component';
import { CreatePersonComponent } from '../pages/person/create-person/create-person.component';
export const appRoutes: Routes = [
  {
    path: '',
    component: AuthComponent,
    children: [
      { path: 'dashboard-input', component: DashboardComponent },

      { path: 'workflow', component: IndexWorkflowComponent },
      { path: 'workflow-create', component: CreateWorkflowComponent },
      { path: 'workflow-edit/:id', component: EditWorkflowComponent },

      { path: 'venue', component: IndexVenueComponent },
      { path: 'venue-create', component: CreateVenueComponent },
      { path: 'venue-edit/:id', component: EditVenueComponent },

      { path: 'vptype', component: IndexVenuePermissiontypeComponent },
      { path: 'vptype-create', component: CreateVenuePermissiontypeComponent },
      { path: 'vptype-edit/:id', component: EditVenuePermissiontypeComponent },

      {
        path: 'control-room',
        loadChildren: () =>
          import(
            'src/app/admin/control-room/control-room-list/control-room-list.module'
          ).then((m) => m.ControlRoomListModule),
      },
      {
        path: 'control-room-create',
        loadChildren: () =>
          import(
            'src/app/admin/control-room/control-room-create/control-room-create.module'
          ).then((m) => m.ControlRoomCreateModule),
      },
      {
        path: 'control-room-edit/:id',
        loadChildren: () =>
          import(
            'src/app/admin/control-room/control-room-edit/control-room-edit.module'
          ).then((m) => m.ControlRoomEditModule),
      },

      { path: '', loadChildren: () => import('src/app/pages/pages.module').then(a => a.PagesModule) },
      { path: 'profile-list', loadChildren: () => import('src/app/admin/master/profile/profile-list/profile-list.module').then(a => a.ProfileListModule) },
      { path: 'profile-create', loadChildren: () => import('src/app/admin/master/profile/profile-create/profile-create.module').then(a => a.ProfileCreateModule) },
      { path: 'profile-edit/:id', loadChildren: () => import('src/app/admin/master/profile/profile-edit/profile-edit.module').then(a => a.ProfileEditModule) },
      
      { path: 'personnel', loadChildren: () => import('../admin/master/personnel/personnel.module').then(a => a.PersonnelModule) },
      { path: 'global', loadChildren: () => import('../global/global.module').then(a => a.GlobalModule) },
      { path: 'organization', loadChildren: () => import('../admin/master/organization/organization.module').then(a => a.OrganizationModule) },
      { path: 'plan', loadChildren: () => import('../admin/master/plan/plan.module').then(m => m.PlanModule) },
      { path: 'leaders', loadChildren: () => import('../admin/master/leaders/leaders.module').then(l => l.LeadersModule) },
      { path: 'allegation', loadChildren: () => import('../admin/master/allegation/allegation.module').then(a => a.AllegationModule) },
      { path: 'enquiry', loadChildren: () => import('../admin/master/enquiry/enquiry.module').then(a => a.EnquiryModule) },
      {path:'reports', loadChildren:()=>import('../admin/master/report/report.module').then(m=>m.ReportModule)},
     // { path: 'personnel-create', loadChildren: () => import('../admin/master/personnel/personnel.module').then(a => a.PersonnelModule) },
      //{ path: 'personnel-edit/:id', loadChildren:()=> import('../admin/master/personnel/personnel-edit/personnel-edit-routing.module').then(a=>a.PersonnelEditRoutingModule) },
      
     
      // { path: 'profile-edit/:id', loadChildren:()=> import('src/app/admin/master/profile/').then(a=>a.ProfileCreateRoutingModule) },



     
      
      // {
      //   path: 'workflow',
      //   loadChildren: () => import('../admin/admin.module').then(AdminModule),
      // },
      // { path: 'material-widgets', loadChildren: () => import('../material-widgets/material-widgets.module').then(m => m.MaterialWidgetsModule) },
      // { path: 'tables', loadChildren: () => import('../tables/tables.module').then(m => m.TablesModule) },
      // { path: 'maps', loadChildren: () => import('../maps/maps.module').then(m => m.MapsModule) },
      // { path: 'charts', loadChildren: () => import('../charts/charts.module').then(m => m.ChartsModule) },
      // // { path: 'chats', loadChildren: () => import('../chats/chat.module').then(m => m.ChatsModule) }, // fix this
      // //{ path: 'mail', loadChildren: () => import('../mail/mail.module').then(m => m.MailModule) }, // fix this
      // { path: 'pages', loadChildren: () => import('../pages/pages.module').then(m => m.PagesModule) },
      // { path: 'forms', loadChildren: () => import('../forms/forms.module').then(m => m.FormModule) }, //fix this
      // { path: 'guarded-routes', loadChildren: () => import('../guarded-routes/guarded-routes.module').then(m => m.GuardedRoutesModule) },
      // // { path: 'editor', loadChildren: () => import('../editor/editor.module').then(m => m.EditorModule) },
      // { path: 'scrumboard', loadChildren: () => import('../scrumboard/scrumboard.module').then(m => m.ScrumboardModule) },
      { path: '**', redirectTo: 'dashboard-input' },
    ],
  },
];
