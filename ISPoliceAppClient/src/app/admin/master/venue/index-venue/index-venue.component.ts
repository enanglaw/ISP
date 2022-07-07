import { Component, OnInit } from '@angular/core';
import { VenueGridDTO } from 'src/app/admin/master/venue.modal';
import { VenueService } from 'src/app/admin/master/venue.service';

@Component({
  selector: 'app-index-venue',
  templateUrl: './index-venue.component.html',
  styleUrls: ['./index-venue.component.scss'],
})
export class IndexVenueComponent implements OnInit {
  venueData: VenueGridDTO[] = [];
  columnsToDisplay: string[] = ['Name', 'PermissionTypes', 'Active', 'actions'];
  constructor(private venueService: VenueService) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.venueService.getVenueForGrid().subscribe(
      (data) => {
        this.venueData = data;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  delete(id: number) {
    this.venueService.deleteVenue(id).subscribe(() => {
      this.getAll();
    });
  }

  undelete(id: number) {
    
    this.venueService.unDeleteVenue(id).subscribe(() => {
      this.getAll();
    });
  }
}
