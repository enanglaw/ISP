import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { GlobalService } from '../../global.service';

@Component({
  selector: 'app-sub-organization-create',
  templateUrl: './sub-organization-create.component.html',
  styleUrls: ['./sub-organization-create.component.scss']
})
export class SubOrganizationCreateComponent implements OnInit {
  constructor(private subOrganizationStatusService: GlobalService,private router: Router) { }

  ngOnInit(): void {
  }

  onSaveChanges(data: any) {
    
    
     this.subOrganizationStatusService.addSubOrganization(data).subscribe(a => {
      Swal.fire(
        'Success',
        'Sub Organization added successfully',
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
        //this.router.navigate(['auth/personnel-list']);
      });
    })
  }
}