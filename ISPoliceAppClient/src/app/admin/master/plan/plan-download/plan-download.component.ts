import { HttpEventType } from '@angular/common/http';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { environment } from 'src/environments/environment';

import { PlanService } from '../plan.service';
import { PlanList } from '../plan.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-plan-download',
  templateUrl: './plan-download.component.html',
  styleUrls: ['./plan-download.component.scss']
})
export class PlanDownloadComponent implements OnInit {
  
  serverUrl=environment.ServerUrl;
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  planData: MatTableDataSource<PlanList>;
  @ViewChild('paginator') paginator: MatPaginator;
  imageUrl = "assets/images/avatars/noavatar.png";
  columnsToDisplay: string[] = ['SlNo','title','actions'];
  
  constructor(private planService: PlanService,) {
    
   }

  ngOnInit(): void {
   this.getAll() ;
  }
  getAll() {
    this.planService.getplanGrid().subscribe(
      (data) => {
        this.planData = new MatTableDataSource(data);
        this.planData.paginator = this.paginator;
        this.resultsLength = this.planData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.planData = new MatTableDataSource();
        this.planData.paginator = this.paginator;
        this.resultsLength = this.planData.data.length;
        this.showProgressBar = false;
      }
    );
  }

  delete(id: number) {
      
    this.showProgressBar = true;
      this.planService.deletePlan(id).subscribe(() =>
      {
      
        this.showProgressBar = false;
        this.getAll();
    }
    ,
    (error) => {
      this.showProgressBar = false;
      this.getAll();
      console.log(error);
        });
        
        
  
  }

  public download(id: number) {
    
   let password = prompt("enter authorization code", "authorization code");
    if (password != null) 
    {
    var isAuthicated= this.planService.authenticate(password)
    if (isAuthicated) {
    // this.downloadStatus.emit( {status: ProgressStatusEnum.START});
    this.planService.downloadPlan(id).subscribe(
      (data:any) => {
        switch (data.type) {
          case HttpEventType.DownloadProgress:
            // this.downloadStatus.emit( {status: ProgressStatusEnum.IN_PROGRESS, percentage: Math.round((data.loaded / data.total) * 100)});
            break;
          case HttpEventType.Response:
            // this.downloadStatus.emit( {status: ProgressStatusEnum.COMPLETE});
            const downloadedFile = new Blob([data.body], { type: data.body.type });
            const a = document.createElement('a');
            a.setAttribute('style', 'display:none;');
            document.body.appendChild(a);
            // a.download = this.fileName;
            a.href = URL.createObjectURL(downloadedFile);
            a.target = '_blank';
            a.click();
            document.body.removeChild(a);
            break;
        }
      },
      error => {
        // this.downloadStatus.emit( {status: ProgressStatusEnum.ERROR});
      }
    );
     
    }
    
  }   
  }

}
