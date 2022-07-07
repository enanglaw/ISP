import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { OrganizationEventDTO } from '../organization.model';
import { OrganizationService } from '../organization.service';

@Component({
  selector: 'app-events-create',
  templateUrl: './events-create.component.html',
  styleUrls: ['./events-create.component.scss']
})
export class EventsCreateComponent implements OnInit {
   
  constructor(private subOrganizationStatusService: OrganizationService, private router: Router) { }

  ngOnInit(): void {
  }

  onSaveChanges(data: any) {
    
     this.subOrganizationStatusService.assignEvent(data).subscribe(a => {
      Swal.fire(
        'Success',
        'Events assigned successfully',
        'success'
      ).then((result) => {
        this.router.navigate(['/auth/organization/events']);
      });
    },
    (error) => {
      (console.log(error))
      Swal.fire(
        'Error',
        'Some Error Occured',
        'error'
      ).then((result) => {
        //this.router.navigate(['auth/personnel-list']);
      });
    })
  }
}