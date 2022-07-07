import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { PersonnelList } from '../personnel.model';
import { PersonnelService } from '../personnel.service';

@Component({
  selector: 'app-personnel-edit',
  templateUrl: './personnel-edit.component.html',
  styleUrls: ['./personnel-edit.component.scss']
})
export class PersonnelEditComponent implements OnInit {

  model: PersonnelList;
  personnelModel: PersonnelList;
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private personnelService: PersonnelService,) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
     

      this.personnelService.getPersonneById(params.id)
        .subscribe((response) => {          
          this.model = response[0];
          this.personnelModel=this.model[0]      
          });
      
    });
  }

  onSaveChanges(data: any) {
    this.personnelService.editPersonnel(data.id, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'personnel updated successfully',
            'success'
          ).then((result) => {
            this.router.navigate(['/auth/personnel/personnel-list']);
         });
        },
        (error) => {
          (console.log(error))
          Swal.fire(
            'Error',
            'Some Error Occured',
            'error'
          ).then((result) => {
            //this.router.navigate(['/auth/leaders/list']);
          });
        });
  }
}
