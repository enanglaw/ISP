import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { OrganizationEventDTO } from '../organization.model';
import { OrganizationService } from '../organization.service';

@Component({
  selector: 'app-organization-create',
  templateUrl: './organization-create.component.html',
  styleUrls: ['./organization-create.component.scss']
})
export class OrganizationCreateComponent implements OnInit {

 
  constructor(private organizationService: OrganizationService,private router: Router) { }

  ngOnInit(): void {
  }

  onSaveChanges(data: any) {
   
     this.organizationService.addOrganization(data).subscribe(a => {
      Swal.fire(
        'Success',
        'Organization saved successfully',
        'success'
      ).then((result) => {
        this.router.navigate(['auth/organization/org-list']);
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
    })
  }
}
