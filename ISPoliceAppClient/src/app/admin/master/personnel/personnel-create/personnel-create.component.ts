import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { PersonnelService } from '../personnel.service';

@Component({
  selector: 'app-personnel-create',
  templateUrl: './personnel-create.component.html',
  styleUrls: ['./personnel-create.component.scss']
})
export class PersonnelCreateComponent implements OnInit {

  constructor(private personnelService: PersonnelService,private router: Router) { }

  ngOnInit(): void {
  }

  onSaveChanges(data: any) {
    
     this.personnelService.addPersonnel(data).subscribe(a => {
      Swal.fire(
        'Success',
        'Personnel saved successfully',
        'success'
      ).then((result) => {
        this.router.navigate(['/auth/personnel/personnel-list']);
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
