import { HttpEventType } from '@angular/common/http';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ProfileList } from '../profile.model';
import { ProfileService } from '../profile.service';

@Component({
  selector: 'app-profile-list',
  templateUrl: './profile-list.component.html',
  styleUrls: ['./profile-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class ProfileListComponent implements OnInit {

  serverUrl=environment.ServerUrl;
  imageUrl = "assets/images/avatars/noavatar.png";
  profileList:ProfileList[]= [];
  totalRecords: number = 10;
  loading: boolean = true;
  columnsToDisplay: string[] = ['No','Name','Hs','alias','actions'];

  constructor(private profileService: ProfileService,) {
    this.getAll()  
   }

  ngOnInit(): void {
  }
  getAll() {
    this.profileService.getprofileGrid().subscribe(
      (data) => {
        this.profileList = data;
        this.totalRecords=data.length;
        this.loading = false;
      },
      (error) => {
        this.loading = false;
        console.log(error);
      }
    );
  }

  delete(id: number) {
    this.loading = true;
    this.profileService.deleteAssociate(id).subscribe(() => {
      this.getAll();
      this.loading = false;
    },
    (error) => {
      this.loading = false;
      console.log(error);
    });
  }

  public download(id:number) {
    // this.downloadStatus.emit( {status: ProgressStatusEnum.START});
    this.profileService.downloadProfile(id).subscribe(
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
