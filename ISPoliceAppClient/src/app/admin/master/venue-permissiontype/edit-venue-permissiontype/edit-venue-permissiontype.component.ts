import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  VenueDropdownDTO,
  VenuePermissionTypeCreationDTO,
} from 'src/app/admin/master/venue.modal';
import { VenueService } from 'src/app/admin/master/venue.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edit-venue-permissiontype',
  templateUrl: './edit-venue-permissiontype.component.html',
  styleUrls: ['./edit-venue-permissiontype.component.scss'],
})
export class EditVenuePermissiontypeComponent implements OnInit {
  errors: string[] = [];
  model: VenuePermissionTypeCreationDTO;
  venuesList: VenueDropdownDTO[] = [];

  constructor(
    private activatedRoute: ActivatedRoute,
    private venueService: VenueService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getVenueForDropdown();
    this.activatedRoute.params.subscribe((params) => {
      this.venueService
        .getVenuePermissionTypeById(params.id)
        .subscribe((venue) => {
          
          this.model = {} as VenuePermissionTypeCreationDTO;
          this.model.venueId = venue.venueId;
          this.model.venuePermissionTypeId = venue.venuePermissionTypeId;
          this.model.venuePermissionTypeName = venue.venuePermissionTypeName;
          this.model.isActive = venue.isActive;
        });
    });
  }

  getVenueForDropdown() {
    this.venueService.getVenueForDropdown().subscribe(
      (obj) => {
        this.venuesList.push(...obj);
      },
      (error) => (this.errors = error)
    );
  }

  saveChanges(data: VenuePermissionTypeCreationDTO) {
    
    this.venueService
      .editVenuePermissionType(this.model.venuePermissionTypeId, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'Venue permission type updated successfully',
            'success'
          ).then((result) => {
            this.router.navigate(['/auth/vptype']);
          });
        },
        (err) => (this.errors = err)
      );
  }
}
