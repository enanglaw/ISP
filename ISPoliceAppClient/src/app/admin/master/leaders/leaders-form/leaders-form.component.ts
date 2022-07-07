import { HttpEventType } from '@angular/common/http';
import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { OrganizationModelList, SubOrganizationModelList } from '../../organization/organization.model';
import { OrganizationService } from '../../organization/organization.service';
import { LeaderEvent, LeaderMedia, LeadersList, PoliticalBackground, } from '../leaders.model';
import { LeadersService } from '../leaders.service';

@Component({
  selector: 'app-leaders-form',
  templateUrl: './leaders-form.component.html',
  styleUrls: ['./leaders-form.component.scss']
})
export class LeadersFormComponent implements OnInit {

 
  @Input() model: LeadersList;
  @Input() organizationListmodel: LeadersList[]=[];
  @Input() heading: any;
  @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();
  @Input() organizationList: OrganizationModelList[]=[];
  @Input() subOrganizationListmodel: SubOrganizationModelList[]=[];
  
  isFormLoded: boolean = false;
  showLoading: boolean = false;
  form: FormGroup;
  count: number=0;
  progress = 0;
  leaderMediaDataSource = new BehaviorSubject<AbstractControl[]>([]);
  leaderMediaDataSourceColumns: string[] = ['No', 'Title', 'Document', 'actions'];
  leaderEventDataSource = new BehaviorSubject<AbstractControl[]>([]);
  leaderEventDataSourceColumns: string[] = ['No', 'Title', 'Description','Event Date', 'actions'];
  leaderPoliticalBackgroundDataSource = new BehaviorSubject<AbstractControl[]>([]);
  leaderPoliticalBackgroundDataSourceColumns: string[] = ['No', 'Position','Position Year', 'actions'];
  martialStatusList: any[] = [];
  genderStatusList: any[] = [];
  religionStatusList: any[] = [];
  constructor(private formBuilder: FormBuilder,
    private leadersService: LeadersService,
    private dialog: MatDialog, private router: Router,
    private ref: ChangeDetectorRef)
  {

  }
 
  ngOnInit(): void {
    this.GetAllOrganization();
    this.getReligionList();
    this.getMaritalStatusList();
    this.getGenderList();
    this.inItForm();
 
  }
  getMaritalStatusList() {
    this.leadersService.getMaritalStatusList().subscribe(
      (obj) => {

        this.martialStatusList.push(...obj);
      },
      (error) => (console.log(error))
    );
  }

getGenderList() {
    this.leadersService.getGenderList().subscribe(
      (obj) => {

        this.genderStatusList.push(...obj);
      },
      (error) => (console.log(error))
    );
  }
getReligionList() {
    this.leadersService.getReligionList().subscribe(
      (obj) => {

        this.religionStatusList.push(...obj);
      },
      (error) => (console.log(error))
    );
  }
  onOrgSelected(event)
  {
    
     this.showLoading = true;
     this.GetOrganizationAndSubOrganizationList(event.value);     
    
  }
  get detailInfo(): FormArray{
    return this.form
    .get("detailInfo") as FormArray
  }
  get leaderPoliticalBackgrounds() : FormArray {
    return this.form.get("leaderPoliticalBackgrounds") as FormArray
  }
  get leaderEvents() : FormArray {
    return this.form.get("leaderEvents") as FormArray
  }
  get leaderMedia(): FormArray {
    return this.form.get("leaderMedia") as FormArray
  }
  public onMediaFileSelected = (files) => {   
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.leadersService.uploadUrl(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
         
          const leaderMedia = this.leaderMedia.at(this.count);
          leaderMedia.patchValue
            ({
              leaderMediaUrl:event.body.dbPath
            })                    
           leaderMedia.updateValueAndValidity();
           this.count++;
        }
      });
  }
      
  GetOrganizationAndSubOrganizationList(Id) {
    let index = 0;
    this.subOrganizationListmodel = [];
    this.leadersService.getOrganizationDataById(Id).subscribe(    
      (data:any) =>
      { 
        let index = 0;
           data.forEach(result => {
         (result[index].subOrganizationCategory.forEach(
          subOrganization => {
            this.subOrganizationListmodel.push(subOrganization)           
            }
          ))
          
          index++;
        }) 
     
      },
      (error) =>
      {
        this.showLoading = false;
        console.log(error);
      }
    );
  }
  GetAllOrganization() {
    this.showLoading = true;
    this.leadersService.getOrganizationDropdown().subscribe(
    
      (data:any) => {  
               
        data.forEach(organization => {
          this.organizationListmodel.push(organization);                  
         })
        this.showLoading = false;
      },
      (error) => {
        this.showLoading = false;
        console.log(error);
      }
    );
  }
  inItForm() {
    this.form = this.formBuilder.group({
      id: [0],
      name: ['', { validators: [Validators.required] }],
      designation: ['', { validators: [Validators.required] }],
      address: [''],
      mobileNumber: [''], 
      organizationLeaderId: [0],
      subOrganizationLeaderId: [0],
      organizationName: [''],
      subOrganizationName: [''],
       // leaderId:[0],      
        leaderName :[''],       
        alias :[''],       
        placeOfBirth :[''],       
        dateOfBirth : [new Date()],
        caste :[''],       
        permanentAddress :[''],     
        presentAddress :[''],       
        nativeDistrict :[''],
        properties :[''],
        strinkingPersonalityTrait :[''],
        presentPartyAffiliation :[''],
        positionInTheParty :[''],
        religionId: [0], 
        religionName:[''],
        genderId: [0],
        genderName:[''],
        leadersId :[0],
        maritalStatusId: [0],
        maritalStatusName:[''],
        leaderEvents: new FormArray([

        ]),
        leaderMedia: new FormArray([

        ]),
        leaderPoliticalBackgrounds: new FormArray([

        ]),
    });
    
    if (this.model !== undefined) {
      
      this.form.patchValue(this.model)      
      let leaderPoliticalBackgrounds = (this.form.get('leaderPoliticalBackgrounds') as FormArray);
      leaderPoliticalBackgrounds.clear(); 
      let politicalBackground = this.model.leaderPoliticalBackgrounds;
      if (politicalBackground != undefined) {
        politicalBackground.forEach(element => {
          leaderPoliticalBackgrounds.push(this.newLeaderPoliticalBackground(element))
        });
      }
     let leaderMedia = (this.form.get('leaderMedia') as FormArray);      
      leaderMedia.clear();
      let media = this.model.leaderMedia;
      if (media != undefined) {
        media.forEach(element => {
          leaderMedia.push(this.newLeaderMedia(element))
        });
      }    
      let events = (this.form.get('leaderEvents') as FormArray);
      let event=this.model.leaderEvents;
       events.clear();           
        event.forEach(element => {
          events.push(this.newLeaderEvents(element))
        })
       
      
      
      

      
    }
   
    this.UpdateLeaderEvent();
    this.updateLeaderPoliticalBackgroundList();
    this.updateLeaderMediaList();
    this.isFormLoded = true;
    

  }
  newLeaderPoliticalBackground(model: PoliticalBackground = {} as PoliticalBackground): import("@angular/forms").AbstractControl {
    let leaderPoliticalBackground = this.formBuilder.group({
      id: model.id ? model.id : 0,
      leaderId: model.leaderId ? model.leaderId : 0,
      position: model.position ? model.position : ['', Validators.required],
      positionYear: model.positionYear ? model.positionYear : [new Date],
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
     
    })
    return leaderPoliticalBackground;
  }

  updateLeaderPoliticalBackgroundList() {
    return this.leaderPoliticalBackgroundDataSource.next(this.getLeaderPoliticalBackgroundList());
  }

  getLeaderPoliticalBackgroundList() {
    return (this.form.get('leaderPoliticalBackgrounds') as FormArray).controls
  }

  onLeaderPoliticalBackgroundDelete(element,index) {    
    (this.form.controls['leaderPoliticalBackgrounds'] as FormArray).removeAt(index);
    this.updateLeaderPoliticalBackgroundList()
  }

  onAddLeaderPoliticalBackground() {
    const leaderPoliticalBackground = this.form.get('leaderPoliticalBackgrounds') as FormArray
    leaderPoliticalBackground.push(this.newLeaderPoliticalBackground())
    this.updateLeaderPoliticalBackgroundList();
  }

  newLeaderMedia(model: LeaderMedia = {} as LeaderMedia): import("@angular/forms").AbstractControl {
    var organizationMedia = this.formBuilder.group({
      id: model.id ? model.id : 0,
      leaderId: model.leaderId ? model.leaderId : 0,
      leaderMediaPath: model.leaderMediaPath ? model.leaderMediaPath : [''],
      title: model.title ? model.title : ['', Validators.required],
      leaderMediaUrl: model.leaderMediaUrl ? model.leaderMediaUrl : ['', Validators.required],
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
      })  
    return organizationMedia;
  }

  updateLeaderMediaList() {
    this.leaderMediaDataSource.next(this.getLeaderMediaList());
  }

  getLeaderMediaList() {
    return (this.form.get('leaderMedia') as FormArray).controls
  }

  onLeaderMediaDelete(element,index) {
    
    (this.form.controls['leaderMedia'] as FormArray).removeAt(index);
    this.updateLeaderMediaList()  
  }

  onAddLeaderMedia() {
    const organizationMedia = this.form.get('leaderMedia') as FormArray
    organizationMedia.push(this.newLeaderMedia())
    this.updateLeaderMediaList();
  }

  newLeaderEvents(model: LeaderEvent = {} as LeaderEvent): import("@angular/forms").AbstractControl {
    var leaderEvent = this.formBuilder.group({
      id: model.id ? model.id : 0,
      leaderId: model.leaderId ? model.leaderId : 0,
      description: model.description ? model.description : '',
      eventDate: model.eventDate ? model.eventDate : [new Date],
      title: model.title ? model.title : ['', Validators.required],
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
      })

    return leaderEvent;
  }



  UpdateLeaderEvent() {
    this.leaderEventDataSource.next(this.getLeaderEventsList());
  }

  getLeaderEventsList() {
    return (this.form.get('leaderEvents') as FormArray).controls
  }

  onLeaderEventDelete(element,index) {
    (this.form.controls['leaderEvents'] as FormArray).removeAt(index);
    this.UpdateLeaderEvent();
  }

  onAddLeaderEvent() {
    const leaderEvent = this.form.get('leaderEvents') as FormArray
    leaderEvent.push(this.newLeaderEvents())
    this.UpdateLeaderEvent();
  }


  saveChanges() {
    if (this.form.valid) {      
     
      this.onSaveChanges.emit(this.form.value)
    }
   
  }

onCancel(){
  this.router.navigate(['/auth/leaders/list']);
}

}

