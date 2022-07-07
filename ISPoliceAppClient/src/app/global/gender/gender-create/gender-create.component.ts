import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { GlobalService } from '../../global.service';

@Component({
  selector: 'app-gender-create',
  templateUrl: './gender-create.component.html',
  styleUrls: ['./gender-create.component.scss']
})
export class GenderCreateComponent implements OnInit {
  constructor(private genderService: GlobalService,private router: Router) { }

  ngOnInit(): void {
  }

  onSaveChanges(data: any) {
    
     this.genderService.addGender(data).subscribe(a => {
      Swal.fire(
        'Success',
        'Gender added successfully',
        'success'
      ).then((result) => {
        this.router.navigate(['auth/global/gender-list']);
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