import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { VenueCreationDTO } from 'src/app/admin/master/venue.modal';
import { VenueService } from 'src/app/admin/master/venue.service';
import { parseWebAPIErrors } from 'src/app/utilities/utils';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-create-venue',
  templateUrl: './create-venue.component.html',
  styleUrls: ['./create-venue.component.scss'],
})
export class CreateVenueComponent implements OnInit {
  errors: string[] = [];
  constructor(private router: Router, private venueService: VenueService) {}

  ngOnInit(): void {}

  saveChanges(venueCreationDTO: VenueCreationDTO) {
    this.venueService.createVenue(venueCreationDTO).subscribe(
      () => {
        Swal.fire('Success', 'Venue created successfully', 'success').then(
          (result) => {
            this.router.navigate(['/auth/venue']);
          }
        );
      },
      (error) => {
        this.errors = parseWebAPIErrors(error);
      }
    );
  }
}
