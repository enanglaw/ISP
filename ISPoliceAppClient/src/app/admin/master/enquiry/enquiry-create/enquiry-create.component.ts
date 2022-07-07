import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { EnquiryList } from '../enquiry.model';
import { EnquiryService } from '../enquiry.service';

@Component({
  selector: 'app-enquiry-create',
  templateUrl: './enquiry-create.component.html',
  styleUrls: ['./enquiry-create.component.scss']
})
export class EnquiryCreateComponent implements OnInit {
  
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private enquiryService: EnquiryService) {
    
    }

    ngOnInit(): void {
    }
    onSaveChanges(data: any) {
    
      this.enquiryService.addAllegationEnquiry(data).subscribe(a => {
       Swal.fire(
         'Success',
         'allegation enquiry saved successfully',
         'success'
       ).then((result) => {
         this.router.navigate(['auth/enquiry/enquiry-list']);
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
     })
   }
  }