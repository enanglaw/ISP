import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ProfileAssociates, ProfileList } from '../profile.model';
import { ProfileService } from '../profile.service';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.scss']
})
export class ProfileEditComponent implements OnInit {
  model: ProfileList;
  errors: any;
  associateList: ProfileAssociates[];

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private profileService: ProfileService,) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.profileService
        .getProfileId(params.id)
        .subscribe((response) => {
          // this.model  = <>controlRoom} as ControlRoomDSR;
          
          this.model = response[0]
          this.associateList = response[1]
          });
      // this.profileService
      //   .getAssociateList(params.id)
      //   .subscribe((profile) => {
      //     // this.model  = <>controlRoom} as ControlRoomDSR;
      //     this.associateList = <ProfileAssociates[]>profile
      //   });
    });
  }

  onSaveChanges(data: any) {
    
    this.profileService
      .editProfile(this.model.id, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'Profile successfully',
            'success'
          ).then((result) => {
            //this.router.navigate(['auth/profile-list']);
          });
        },
        (error) => {
          (console.log(error))
          Swal.fire(
            'Error',
            'Some Error Occured',
            'error'
          ).then((result) => {
            this.router.navigate(['auth/profile-list']);
          });
        });
  }
}
