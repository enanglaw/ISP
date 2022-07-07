import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ProfileService } from '../profile.service';

@Component({
  selector: 'app-profile-create',
  templateUrl: './profile-create.component.html',
  styleUrls: ['./profile-create.component.scss']
})
export class ProfileCreateComponent implements OnInit {

  constructor(private profileService: ProfileService,private router: Router) { }

  ngOnInit(): void {
  }

  onSaveChanges(data: any) {
    
     this.profileService.addProfile(data).subscribe(a => {
      Swal.fire(
        'Success',
        'Profile successfully',
        'success'
      ).then((result) => {
        this.router.navigate(['auth/profile-list']);
      });
    },
    (error) => {
      (console.log(error))
      Swal.fire(
        'Error',
        'Some Error Occured',
        'error'
      ).then((result) => {
        //this.router.navigate(['auth/profile-list']);
      });
    })
  }
}
