import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ControlRoomDSR } from '../control-room.model';
import { ControlRoomService } from '../control-room.service';

@Component({
  selector: 'app-control-room-edit',
  templateUrl: './control-room-edit.component.html',
  styleUrls: ['./control-room-edit.component.scss']
})
export class ControlRoomEditComponent implements OnInit {
  model: ControlRoomDSR;
  errors: any;

  constructor(
    private controlRoomService: ControlRoomService,

    private activatedRoute: ActivatedRoute,

    private router: Router
    ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.controlRoomService
        .getControlRoomById(params.id)
        .subscribe((controlRoom) => {
          // this.model  = <>controlRoom} as ControlRoomDSR;
          this.model  = <ControlRoomDSR>controlRoom
        });
    });
  }

  onSaveChanges(data: any) {
    
    this.controlRoomService
      .editControlRoom(this.model.controlRoomId, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'DRS updated successfully',
            'success'
          ).then((result) => {
            this.router.navigate(['auth/control-room']);
          });
        },
        (err) => (this.errors = err)
      );
  }
}
