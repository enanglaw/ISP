import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { GlobalService } from '../../global.service';

@Component({
  selector: 'app-marital-status-create',
  templateUrl: './marital-status-create.component.html',
  styleUrls: ['./marital-status-create.component.scss']
})
export class MaritalStatusCreateComponent implements OnInit {
  constructor(private maritalStatusService: GlobalService,private router: Router) { }

  ngOnInit(): void {
  }

  onSaveChanges(data: any) {
    
     this.maritalStatusService.addMaritalStatus(data).subscribe(a => {
      Swal.fire(
        'Success',
        'Marital status added successfully',
        'success'
      ).then((result) => {
        this.router.navigate(['auth/global/marital-list']);
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