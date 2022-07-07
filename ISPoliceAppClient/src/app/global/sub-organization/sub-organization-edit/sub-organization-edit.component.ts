import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { OrganizationList, OrganizationTransaction, SubOrganization, SubOrganizationData, SubOrganizationDTO } from '../../global.model';
import { GlobalService } from '../../global.service';

@Component({
  selector: 'app-sub-organization-edit',
  templateUrl: './sub-organization-edit.component.html',
  styleUrls: ['./sub-organization-edit.component.scss']
})

export class SubOrganizationEditComponent implements OnInit {
  model: SubOrganization;
  subOrganizationList: SubOrganization[] = [];

  errors: any;
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private SubOrganizationService: GlobalService) {
    
    }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.SubOrganizationService
        .getSubOrganizationById(params.id)
        .subscribe((response) => {
          console.log(response);
          this.model = response[0];
          this.subOrganizationList.push(this.model)
        
        });
    });
  }
  onCancel()
	{
	  this.router.navigate(['/auth/global/sub-organization-edit']);
	}
  onSaveChanges(data: any) {
    
    this.SubOrganizationService
      .editSubOrganization(data.id, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'Sub Organization updated successfully',
            'success'
          ).then((result) => {
            this.router.navigate(['/auth/global/sub-organization-list']);
          });
        },
        (error) => {
          (console.log(error))
          Swal.fire(
            'Error',
            'Some Error Occured',
            'error'
          ).then((result) => {
            //this.router.navigate(['/auth/global/sub-organization-list']);
          });
        });
  }
}
