import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { OrganizationAssociate, OrganizationDTO, OrganizationList } from '../organization.model';
import { OrganizationService } from '../organization.service';

@Component({
  selector: 'app-organization-edit',
  templateUrl: './organization-edit.component.html',
  styleUrls: ['./organization-edit.component.scss']
})
export class OrganizationEditComponent implements OnInit {

  model: OrganizationList;
  organizationModel:OrganizationDTO
  organizationList:OrganizationAssociate[]=[]
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private organizationService: OrganizationService,) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.organizationService.getOrganizationById(params.id)
        .subscribe((response) => {          
          this.model = response[0];
          this.organizationModel=this.model[0]      
          });
      
    });
  }

  onSaveChanges(data: any) {
    
    this.organizationService.editOrganization(data.organizationId, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'Organization updated successfully',
            'success'
          ).then((result) => {
            this.router.navigate(['/auth/organization/org-list']);
         });
        },
        (error) => {
          (console.log(error))
          Swal.fire(
            'Error',
            'Some Error Occured',
            'error'
          ).then((result) => {
            this.router.navigate(['/auth/organization/org-list']);
          });
        });
  }
}
