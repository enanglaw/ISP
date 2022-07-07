import { Component, OnInit } from '@angular/core';
import { ControlRoomService } from '../control-room.service';
import 'rxjs/add/operator/mergeMap'
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
@Component({
  selector: 'app-control-room-create',
  templateUrl: './control-room-create.component.html',
  styleUrls: ['./control-room-create.component.scss']
})
export class ControlRoomCreateComponent implements OnInit {


  constructor( private controlRoomService: ControlRoomService,private router: Router) { }

  ngOnInit(): void {
    

  }

  onSaveChanges(controlRommObject) {
    this.controlRoomService.saveControlRoom(controlRommObject).subscribe(
      (obj) => {
        Swal.fire(
          'Success',
          'DRS created successfully',
          'success'
        ).then((result) => {
          this.router.navigate(['/auth/control-room-create']);
        });
      },
      (error) => (console.log(error))
    );
  }
  
  
}
