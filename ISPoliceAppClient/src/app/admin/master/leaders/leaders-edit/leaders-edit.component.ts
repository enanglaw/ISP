import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { LeaderList, LeadersDTO, LeadersList } from '../leaders.model';
import { LeadersService } from '../leaders.service';

@Component({
  selector: 'app-leaders-edit',
  templateUrl: './leaders-edit.component.html',
  styleUrls: ['./leaders-edit.component.scss']
})
export class LeadersEditComponent implements OnInit {
  model: LeaderList;
  leadersModel: LeaderList;
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private leadersService: LeadersService,) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
     

      this.leadersService.getLeadersById(params.id)
        .subscribe((response) => {          
          this.model = response[0];
          this.leadersModel=this.model[0]      
          });
      
    });
  }

  onSaveChanges(data: any) {
    this.leadersService.editLeaders(data.id, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'leaders updated successfully',
            'success'
          ).then((result) => {
            this.router.navigate(['/auth/leaders/list']);
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
