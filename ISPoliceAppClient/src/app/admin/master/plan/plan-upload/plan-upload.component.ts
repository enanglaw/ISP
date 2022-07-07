import { HttpEventType } from '@angular/common/http';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { environment } from 'src/environments/environment';

import { PlanService } from '../plan.service';
import { PlanList } from '../plan.model';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-plan-upload',
  templateUrl: './plan-upload.component.html',
  styleUrls: ['./plan-upload.component.scss']
})
export class PlanUploadComponent implements OnInit {

  constructor(private planService: PlanService,private router: Router) {
    
   }

  ngOnInit(): void {
   
  }
 

  onSaveChanges(data: any) {
   this.planService.addPlan(data.plans).subscribe(a => {
     Swal.fire(
       'Success',
       'Plan added successfully',
       'success'
     ).then((result) => {
       this.router.navigate(['auth/plan/download']);
     });
   },
   (error) => {
     (console.log(error))
     Swal.fire(
       'Error',
       'Some Error Occured',
       'error'
     ).then((result) => {
       //this.router.navigate(['auth/profile-list']);
     });
   })
 }

}


