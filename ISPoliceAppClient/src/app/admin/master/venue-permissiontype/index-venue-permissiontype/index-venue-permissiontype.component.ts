import { Component, OnInit } from '@angular/core';
import { VenuePermissionGridDTO } from 'src/app/admin/master/venue.modal';
import { VenueService } from 'src/app/admin/master/venue.service';

@Component({
  selector: 'app-index-venue-permissiontype',
  templateUrl: './index-venue-permissiontype.component.html',
  styleUrls: ['./index-venue-permissiontype.component.scss'],
})
export class IndexVenuePermissiontypeComponent implements OnInit {
  venueData: VenuePermissionGridDTO[] = [];
  columnsToDisplay: string[] = ['PermissionType', 'Venue', 'Active', 'actions'];
  constructor(private venueService: VenueService) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.venueService.getVenuePermissionTypeForGrid().subscribe(
      (data) => {
        this.venueData = data;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  delete(id: number) {
    this.venueService.deleteVenuePermissionType(id).subscribe(() => {
      this.getAll();
    });
  }

  undelete(id: number) {
    
    this.venueService.unDeleteVenuePermissionType(id).subscribe(() => {
      this.getAll();
    });
  }
}
