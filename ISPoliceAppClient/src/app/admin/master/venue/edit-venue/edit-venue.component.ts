import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VenueCreationDTO } from 'src/app/admin/master/venue.modal';
import { VenueService } from 'src/app/admin/master/venue.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edit-venue',
  templateUrl: './edit-venue.component.html',
  styleUrls: ['./edit-venue.component.scss'],
})
export class EditVenueComponent implements OnInit {
  errors: string[] = [];
  model: VenueCreationDTO;

  constructor(
    private activatedRoute: ActivatedRoute,
    private venueService: VenueService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.venueService.getVenueById(params.id).subscribe((venue) => {
        this.model = {} as VenueCreationDTO;
        this.model.venueId = venue.venueId;
        this.model.venueName = venue.venueName;
        this.model.isActive = venue.isActive;
      });
    });
  }

  saveChanges(venueCreationDTO: VenueCreationDTO) {
    this.venueService.editVenue(this.model.venueId, venueCreationDTO).subscribe(
      () => {
        Swal.fire('Success', 'Venue updated successfully', 'success').then(
          (result) => {
            this.router.navigate(['/auth/venue']);
          }
        );
      },
      (err) => (this.errors = err)
    );
  }
}
