import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  VenueDropdownDTO,
  VenuePermissionTypeCreationDTO,
} from 'src/app/admin/master/venue.modal';
import { VenueService } from 'src/app/admin/master/venue.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-create-venue-permissiontype',
  templateUrl: './create-venue-permissiontype.component.html',
  styleUrls: ['./create-venue-permissiontype.component.scss'],
})
export class CreateVenuePermissiontypeComponent implements OnInit {
  errors: string[] = [];
  venuesList: VenueDropdownDTO[] = [];

  constructor(private venueService: VenueService, private router: Router) {}

  ngOnInit(): void {
    this.getVenueForDropdown();
    console.log(this.venuesList);
  }

  getVenueForDropdown() {
    this.venueService.getVenueForDropdown().subscribe(
      (obj) => {
        this.venuesList.push(...obj);
      },
      (error) => (this.errors = error)
    );
  }

  saveChanges(venuePermissionData: VenuePermissionTypeCreationDTO) {
    console.log(venuePermissionData);
    
    this.venueService.createVenuePermissionType(venuePermissionData).subscribe(
      (obj) => {
        console.log(obj);
        Swal.fire(
          'Success',
          'Venue permission type created successfully',
          'success'
        ).then((result) => {
          this.router.navigate(['/auth/vptype']);
        });
      },
      (err) => (this.errors = err)
    );
  }
}
