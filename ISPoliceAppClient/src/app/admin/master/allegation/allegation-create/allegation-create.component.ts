import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { AllegationService } from '../allegation.service';

@Component({
  selector: 'app-allegation-create',
  templateUrl: './allegation-create.component.html',
  styleUrls: ['./allegation-create.component.scss']
})
export class AllegationCreateComponent implements OnInit {
  constructor(private allegationService: AllegationService, private router: Router) { }

  ngOnInit(): void {
  }

    
  onSaveChanges(data: any) {
    console.log("before")
    console.log(data)
    console.log("After ")
    this.allegationService.addAllegation(data).subscribe(a => {
     Swal.fire(
       'Success',
       'allegation saved successfully',
       'success'
     ).then((result) => {
       this.router.navigate(['auth/allegation/allegation-list']);
     });
   },
   (error) => {
     (console.log(error))
     Swal.fire(
       'Error',
       'Some Error Occured',
       'error'
     ).then((result) => {
       this.router.navigate(['/auth/allegation/allegation-list']);
     });
   })
 }
}