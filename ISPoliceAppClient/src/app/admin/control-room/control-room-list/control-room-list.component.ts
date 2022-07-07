import { Component, OnInit } from '@angular/core';
import { ControlRoomDSRModel } from 'src/app/shared/masterData.model';
import { ControlRoomList } from '../control-room.model';
import { ControlRoomService } from '../control-room.service';

@Component({
  selector: 'app-control-room-list',
  templateUrl: './control-room-list.component.html',
  styleUrls: ['./control-room-list.component.scss']
})
export class ControlRoomListComponent implements OnInit {

//   controlRoomId: num
// date: string;
// pSName: number;
// givenBy: string;
// takenBy: string;
// subject: string;
// caseNo: string;
// complainant: strin
  complainantAddress
  controlRoomData: ControlRoomDSRModel[] = [];
  columnsToDisplay: string[] = ['No','PSName','Date','CaseNo','Complainant', 'GivenBy', 'TakenBy', 'actions'];

  constructor(private controlRoomService:ControlRoomService) { }

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.controlRoomService.getControlRoomGrid().subscribe(
      (data) => {
        this.controlRoomData = data;
      },
      (error) => {
        console.log(error);
      }
    );
  }
  delete(id: number) {
    // this.venueService.deleteVenue(id).subscribe(() => {
    //   this.getAll();
    // });
  }

  undelete(id: number) {
    
    // this.venueService.unDeleteVenue(id).subscribe(() => {
    //   this.getAll();
    // });
  }

}
