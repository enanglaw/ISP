import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { OrganizationEventDTO, SubOrganization } from '../organization.model';
import { OrganizationService } from '../organization.service';

@Component({
  selector: 'app-events-edit',
  templateUrl: './events-edit.component.html',
  styleUrls: ['./events-edit.component.scss']
})
export class EventsEditComponent implements OnInit {
  model: OrganizationEventDTO;
  eventsModel: OrganizationEventDTO;

  errors: any;
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private SubOrganizationService: OrganizationService) {
    
    }

  ngOnInit(): void {

    this.activatedRoute.params.subscribe((params) => {
      
      this.SubOrganizationService        
        .getOrganizationEventById(params.id)
        .subscribe((response) => {;
          this.model = response[0];
          this.eventsModel=this.model[0]    
        
        });
    });
  }
  onCancel()
	{
	  this.router.navigate(['/auth/global/sub-organization-edit']);
	}
  onSaveChanges(data: any) {
    
    this.SubOrganizationService.editOrganizationEvent(data.id, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'Sub Organization Event updated successfully',
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
            this.router.navigate(['/auth/organization/events']);
          });
        });
  }
}