import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { AllegationList } from '../allegation.model';
import { AllegationService } from '../allegation.service';

@Component({
  selector: 'app-allegation-edit',
  templateUrl: './allegation-edit.component.html',
  styleUrls: ['./allegation-edit.component.scss']
})
export class AllegationEditComponent implements OnInit {
  model: AllegationList;
  allegationModel: AllegationList;

  errors: any;
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private allegationService: AllegationService) {
    
    }

  ngOnInit(): void {

    this.activatedRoute.params.subscribe((params) => {
      
      this.allegationService        
        .getAllegationById(params.id)
        .subscribe((response) => {;
          this.model = response[0];
          this.allegationModel=this.model[0]    
        
        });
    });
  }
  onCancel()
	{
	  this.router.navigate(['/auth/allegation/allegation-list']);
	}
  onSaveChanges(data: any) {
    
    this.allegationService.editAllegation(data.id, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'Allegation updated successfully',
            'success'
          ).then((result) => {
            this.router.navigate(['/auth/allegation/allegation-list']);
          });
        },
        (error) => {
          (console.log(error))
          Swal.fire(
            'Error',
            'Some Error Occured',
            'error'
          ).then((result) => {
            this.router.navigate(['/auth/allegation/allegation-list']);
          });
        });
  }
}