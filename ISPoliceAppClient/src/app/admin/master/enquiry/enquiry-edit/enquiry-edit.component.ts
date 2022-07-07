import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { EnquiryList } from '../enquiry.model';
import { EnquiryService } from '../enquiry.service';

@Component({
  selector: 'app-enquiry-edit',
  templateUrl: './enquiry-edit.component.html',
  styleUrls: ['./enquiry-edit.component.scss']
})
export class EnquiryEditComponent implements OnInit {
  model: EnquiryList;
  allegationModel: EnquiryList;
  errors: any;
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private enquiryService: EnquiryService) {
    
    }

  ngOnInit(): void {

    this.activatedRoute.params.subscribe((params) => {
      
      this.enquiryService
        .getAllegationEnquiryById(params.id)
        .subscribe((response) => {;
          this.model = response[0];
          this.allegationModel=this.model[0]    
        
        });
    });
  }
  onCancel()
	{
	  this.router.navigate(['/auth/enquiry/enquiry-list']);
	}
  onSaveChanges(data: any) {
    
    this.enquiryService.editAllegationEnquiry(data.id, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'Allegation Enquiry updated successfully',
            'success'
          ).then((result) => {
            this.router.navigate(['/auth/enquiry/enquiry-list']);
          });
        },
        (error) => {
          (console.log(error))
          Swal.fire(
            'Error',
            'Some Error Occured',
            'error'
          ).then((result) => {
            this.router.navigate(['/auth/enquiry/enquiry-list']);
          });
        });
  }
}
