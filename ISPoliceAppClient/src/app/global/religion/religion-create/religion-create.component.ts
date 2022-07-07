import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

import { GlobalService } from '../../global.service';
@Component({
  selector: 'app-religion-create',
  templateUrl: './religion-create.component.html',
  styleUrls: ['./religion-create.component.scss']
})
export class ReligionCreateComponent implements OnInit {

  constructor(private religionService: GlobalService,private router: Router) { }

  ngOnInit(): void {
  }

  onSaveChanges(data: any) {
    
     this.religionService.addReligion(data).subscribe(a => {
      Swal.fire(
        'Success',
        ' added successfully',
        'success'
      ).then((result) => {
        this.router.navigate(['auth/global/religion-list']);
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