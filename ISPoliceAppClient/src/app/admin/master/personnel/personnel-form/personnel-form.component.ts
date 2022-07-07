import { HttpEventType } from '@angular/common/http';
import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import {PersonnelList,PersonnelCaseDetails,PersonnelPostings,
  PersonnelPreviousAllegations,PersonnelWarningOrPunishments, PersonnelChildrens, PersonnelSpouses, PersonnelEducationalBackgrounds, PersonnelGallantryAwards  } from '../personnel.model'; 
import { PersonnelService } from '../personnel.service';
import { startWith, debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-personnel-form',
  templateUrl: './personnel-form.component.html',
  styleUrls: ['./personnel-form.component.scss']
})
export class PersonnelFormComponent implements OnInit {
  
  @Input() model: PersonnelList;
  @Input() heading: any;
  @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();
  martialStatusList: any[] = [];
  genderStatusList: any[] = [];
  religionStatusList: any[] = [];
  isFormLoded: boolean = false;
  showLoading: boolean = false;
  form: FormGroup;
  count: number = 0;
  allegedDocumenCount: number = 0;
  warningCount: number = 0;
  progress = 0;
  imageUrl = environment.ServerUrl+"Resources/images/noavatar.png";
  
  personnelSpouseDataSource = new BehaviorSubject<AbstractControl[]>([]);
  personnelSpouseDataSourceColumns: string[] = ['No', 'FirstName','LastName', 'actions'];
  
  personnelChildrenDataSource = new BehaviorSubject<AbstractControl[]>([]);
  personnelChildrenDataSourceColumns: string[] = ['No', 'FirstName', 'LastName', 'actions'];
 
  personnelWarningDataSource = new BehaviorSubject<AbstractControl[]>([]);
  personnelWarningDataSourceColumns: string[] = ['No','Title','WarningDate','Document', 'actions'];
  
  personnelEducationDataSource = new BehaviorSubject<AbstractControl[]>([]);
  personnelEducationDataSourceColumns: string[] = ['No','Institution','Qualification','graduationYear','actions'];

 
  gallantryAwardsDataSource = new BehaviorSubject<AbstractControl[]>([]);
  gallantryAwardsDataSourceColumns: string[] = ['No','Title','IssuingDate','IssuingAuthority','Document','actions'];
 
  personnelPostingsDataSource = new BehaviorSubject<AbstractControl[]>([]);
  personnelPostingsDataSourceColumns: string[] = ['No','Post','Place','From','To' ,'actions'];

  previousAllegationsDataSource = new BehaviorSubject<AbstractControl[]>([]);
  previousAllegationsDataSourceColumns: string[] = ['No','Description','Result','AllegedDate','Document','actions'];

  allegationEnquiriesDataSource = new BehaviorSubject<AbstractControl[]>([]);
  allegationEnquiriesDataSourceColumns: string[] = ['No','Title', 'Description', 'Date', 'actions'];

  caseDetailDataSource = new BehaviorSubject<AbstractControl[]>([]);
  caseDetailDataSourceColumns: string[] = ['No','Title','CaseNumber','CaseSection','CurrentStatus', 'actions'];
 

  constructor(private formBuilder: FormBuilder, private personnelService: PersonnelService,
    private dialog: MatDialog,private router: Router,private ref: ChangeDetectorRef) {

  }


  ngOnInit(): void
  {
    this.getReligionList();
    this.getMaritalStatusList();
    this.getGenderList();
    
    this.inItForm();
  
  }
  get personnelWarning(): FormArray {
    return this.form.get("personnelWarningOrPunishments") as FormArray
  }
  get gallantryAward(): FormArray {
    return this.form.get("personnelGallantryAwards") as FormArray
  }
  get perviousAllegation(): FormArray {
    return this.form.get("personnelPreviousAllegations") as FormArray
  }
 public onWarningMediaFileSelected=(files)=>{
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.personnelService.uploadPhoto(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          const personnelWarning = this.personnelWarning.at(this.warningCount);
          personnelWarning.patchValue
            ({
              attachmentUrl:event.body.dbPath
            })                    
            personnelWarning.updateValueAndValidity();
           this.warningCount++;
        }
      });
  }
 public onAwardMediaFileSelected=(files)=> {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.personnelService.uploadPhoto(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
        
          const gallantryAward = this.gallantryAward.at(this.count);
          gallantryAward.patchValue
            ({
              awardDocumentUrl:event.body.dbPath
            })                    
          gallantryAward.updateValueAndValidity();
           this.count++;
        
          
        }
      });
  }
 
 
 public onPreviouAllegationMediaFileSelected=(files)=> {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.personnelService.uploadPhoto(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {

          const perviousAllegation = this.perviousAllegation.at(this.allegedDocumenCount);
          perviousAllegation.patchValue
            ({
              attachmentUrl:event.body.dbPath
            })                    
            perviousAllegation.updateValueAndValidity();
           this.allegedDocumenCount++;
        }
      });
    
  }
 
  
  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.personnelService.uploadPhoto(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.form.patchValue({
            personnelPhotoUrl: event.body.dbPath
          });
          this.imageUrl = environment.ServerUrl + event.body.dbPath;
        
        }
      });
  }
  get f() {
    return this.form.controls;
  }
   
  getMaritalStatusList() {
    this.personnelService.getMaritalStatusList().subscribe(
      (obj) => {

        this.martialStatusList.push(...obj);
      },
      (error) => (console.log(error))
    );
  }

getGenderList() {
    this.personnelService.getGenderList().subscribe(
      (obj) => {

        this.genderStatusList.push(...obj);
      },
      (error) => (console.log(error))
    );
  }
getReligionList() {
    this.personnelService.getReligionList().subscribe(
      (obj) => {

        this.religionStatusList.push(...obj);
      },
      (error) => (console.log(error))
    );
  }
  inItForm() {
    this.form = this.formBuilder.group({
        
        name: ['', { validators: [Validators.required] }],
   
        
        id: 0,
        personnelId: [0],        
        permanentAddress: ['',{ validators: [Validators.required] }],
        presentAddress: [''],
        personnelPhotoUrl: ["Resources/images/noavatar.png"],
        fatherName: [''],
      motherName: [''], 
      dateOfJoiningPresentPosting: [new Date()],
        dateOfEnlistment: [new Date()],
        dateOffBirth: [new Date()],
        currentRank: ['', { validators: [Validators.required] }],
        personalNumber: [''],
        contactNumber: [''],
        email: ['', { validators: [Validators.required] }],       
        presentPosting:[''],
        religionId:[0],
        religionName:[''],
        genderId:[0],
        genderName:[''],
        maritalStatusId:[0],
        martialStatusName:[''],
      
      personnelSpouses: new FormArray([
      ]),
      personnelChildrens: new FormArray([
      ]),      
      personnelCaseDetails: new FormArray([
      ]),
      personnelWarningOrPunishments: new FormArray([
      ]),
      personnelEducationalBackgrounds: new FormArray([
      ]),
      personnelGallantryAwards: new FormArray([
      ]),
      personnelPostings: new FormArray([
      ]),
      personnelPreviousAllegations: new FormArray([
      ]),
    });
   
    if (this.model !== undefined) {
      this.imageUrl=environment.ServerUrl+ this.model.personnelPhotoUrl
      this.form.patchValue(this.model);
      let personnelSpouses = (this.form.get('personnelSpouses') as FormArray);
      personnelSpouses.clear();
      let spouse = this.model.personnelSpouses;
      spouse.forEach(element => {
        personnelSpouses.push(this.newPersonnelSpouse(element))
      });
      
      let personnelChildrens = (this.form.get('personnelChildrens') as FormArray);
      personnelChildrens.clear();
      let children = this.model.personnelChildrens;
      children.forEach(element => {
        personnelChildrens.push(this.newPersonnelChildrens(element))
      })        
     
         

      let newPerviousAllegations = (this.form.get('personnelPreviousAllegations') as FormArray);
      newPerviousAllegations.clear();
      let previousAllegation = this.model.personnelPreviousAllegations;
      previousAllegation.forEach(element => {
        newPerviousAllegations.push(this.newPerviousAllegations(element))
      });

      let newpersonnelPostings = (this.form.get('personnelPostings') as FormArray);
      newpersonnelPostings.clear();
      let personnelPosting = this.model.personnelPostings;
      personnelPosting.forEach(element => {
        newpersonnelPostings.push(this.newPersonnelPostings(element))
      });

      let newgallantryAward = (this.form.get('personnelGallantryAwards') as FormArray);
      newgallantryAward.clear();
      let gallantryAward = this.model.personnelGallantryAwards;
      gallantryAward.forEach(element => {
        newgallantryAward.push(this.newGallantryAward(element))
      });

      let newpersonnelEducation = (this.form.get('personnelEducationalBackgrounds') as FormArray);
      newpersonnelEducation.clear();
      let education = this.model.personnelEducationalBackgrounds;
      education.forEach(element => {
        newpersonnelEducation.push(this.newPersonnelEducation(element))
      });

      let newpersonnelWarningModel = (this.form.get('personnelWarningOrPunishments') as FormArray);
      newpersonnelWarningModel.clear();
      let warningOrPunishment = this.model.personnelWarningOrPunishments;
      warningOrPunishment.forEach(element => {
        newpersonnelWarningModel.push(this.newPersonnelWarning(element))
      });

      let newcaseDetail = (this.form.get('personnelCaseDetails') as FormArray);
      newcaseDetail.clear();
      let caseDetails = this.model.personnelCaseDetails;
      caseDetails.forEach(element => {
        newcaseDetail.push(this.newcaseDetail(element))
      });

      
    }
   
    
    
    this.updateCaseDetailList();
    this.updatePersonnelWarning();
    this.updateGallantryAward();
    this.updateSpouseList();
    this.updatePreviousAllegations();
    this.updatePersonnelPostings();
    this.updatePersonnelEducation();
    this.updatePersonnelChildrenList();
    
    this.isFormLoded = true;
    

  }
 
 
 
  
  newPersonnelWarning(model: PersonnelWarningOrPunishments= {} as PersonnelWarningOrPunishments): import("@angular/forms").AbstractControl {
    var personnelWarningOrPunishments = this.formBuilder.group({
      id: model.id ? model.id : 0,
      personnelId: model.personnelId ? model.personnelId : 0,
      attachmentUrl: model.attachmentUrl ? model.attachmentUrl : '',
      warningDate:model.warningDate? model.warningDate:[new Date()],
      title: model.title ? model.title : ['', Validators.required],
        isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return personnelWarningOrPunishments;
  }
  updatePersonnelWarning() {
    return this.personnelWarningDataSource.next(this.getPersonnelWarningList());
  }
  getPersonnelWarningList() {
    return (this.form.get('personnelWarningOrPunishments') as FormArray).controls
  }
  onAddPersonnelWarning() {
    const warningOrPunishments = this.form.get('personnelWarningOrPunishments') as FormArray
    warningOrPunishments.push(this.newPersonnelWarning())
    this.updatePersonnelWarning();
  }
  onPersonnelWarningDelete(element, index)
  {
    
    (this.form.controls['personnelWarningOrPunishments'] as FormArray).removeAt(index);
     this.updatePersonnelWarning();
    
  }
    
  newPersonnelEducation(model: PersonnelEducationalBackgrounds= {} as PersonnelEducationalBackgrounds): import("@angular/forms").AbstractControl {
    var personnelEducationBackground = this.formBuilder.group({
      id: model.id ? model.id : 0,
      personnelId: model.personnelId ? model.personnelId : 0,
     
      admissionYear: model.admissionYear?model.admissionYear:(new Date()),
      institutionName: model.institutionName?model.institutionName:['', Validators.required],
      courseOfStudy: model.courseOfStudy?model.courseOfStudy:'',
      qualificationName: model.qualificationName?model.qualificationName:['', Validators.required],
      graduationYear:model.admissionYear?model.admissionYear:(new Date()),
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return personnelEducationBackground;
  }
  updatePersonnelEducation() {
    return this.personnelEducationDataSource.next(this.getPersonnelEducationList());
  }
  getPersonnelEducationList() {
    return (this.form.get('personnelEducationalBackgrounds') as FormArray).controls
  }
  onAddPersonnelEducation() {
    const personnelEducation = this.form.get('personnelEducationalBackgrounds') as FormArray
    personnelEducation.push(this.newPersonnelEducation())
    this.updatePersonnelEducation();
  }
  onPersonnelEducationDelete(element, index)
  {
    
    (this.form.controls['personnelEducationalBackgrounds'] as FormArray).removeAt(index);
     this.updatePersonnelEducation();
    
  }
   newGallantryAward(model: PersonnelGallantryAwards= {} as PersonnelGallantryAwards): import("@angular/forms").AbstractControl {
        var personnelGallantryAward = this.formBuilder.group({
          id: model.id ? model.id : 0,
          personnelId: model.personnelId ? model.personnelId : 0,
          title: model.title?model.title:['', Validators.required],
          issueingAuthority:model.issueingAuthority?model.issueingAuthority:['', Validators.required], 
          issuingDate:model.issuingDate ? model.issuingDate: (new Date()),
          awardDocumentUrl:model.awardDocumentUrl?model.awardDocumentUrl:'',
          
          isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
        })
    
        return personnelGallantryAward;
      }
     updateGallantryAward() {
        return this.gallantryAwardsDataSource.next(this.getGallantryAwardList());
      }
    
      getGallantryAwardList() {
        return (this.form.get('personnelGallantryAwards') as FormArray).controls
      }
      onAddGallantryAwards() {
        const gallantryAward = this.form.get('personnelGallantryAwards') as FormArray
        gallantryAward.push(this.newGallantryAward())
        this.updateGallantryAward();
      }
      onGallantryAwardDelete(element, index)
      {
        
        (this.form.controls['personnelGallantryAwards'] as FormArray).removeAt(index);
         this.updateGallantryAward();
        
      }
      
  
  newPersonnelPostings(model: PersonnelPostings= {} as PersonnelPostings): import("@angular/forms").AbstractControl {
    var personnelPosting = this.formBuilder.group({
      id: model.id ? model.id : 0,
      personnelId: model.personnelId ? model.personnelId : 0,
     to: model.to ? model.to : (new Date()),
    from:model.from ? model.from : (new Date()),
    place:model.place ?model.place:['', Validators.required],
    post:model.post?model.post:['', Validators.required],
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return personnelPosting;
  }

  updatePersonnelPostings() {
    return this.personnelPostingsDataSource.next(this.getPersonnelPostingList());
  }

  getPersonnelPostingList() {
    return (this.form.get('personnelPostings') as FormArray).controls
  }
  onAddPersonnelPosting() {
    const personnelPosting = this.form.get('personnelPostings') as FormArray
    personnelPosting.push(this.newPersonnelPostings())
    this.updatePersonnelPostings();
  }
  onPersonnelPostingDelete(element, index)
  {
    
    (this.form.controls['personnelPostings'] as FormArray).removeAt(index);
     this.updatePersonnelPostings();
    
  }
  

  newPerviousAllegations(model: PersonnelPreviousAllegations = {} as PersonnelPreviousAllegations): import("@angular/forms").AbstractControl {
    var previousAllegations = this.formBuilder.group({
      id: model.id ? model.id : 0,
      personnelId: model.personnelId ? model.personnelId : 0,
      attachmentUrl: model.attachmentUrl ? model.attachmentUrl : '', 
      allegedDate:model.allegedDate ? model.allegedDate : (new Date()),
      result: model.result ? model.result:['', Validators.required],
      description:  model.description?model.description:'',   
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return previousAllegations;
  }
  updatePreviousAllegations() {
    return this.previousAllegationsDataSource.next(this.getPreviousAllegationList());
  }
  getPreviousAllegationList() {
    return (this.form.get('personnelPreviousAllegations') as FormArray).controls
  }
  onAddPreviousAllegation() {
    const personnelPreviousAllegation = this.form.get('personnelPreviousAllegations') as FormArray
    personnelPreviousAllegation.push(this.newPerviousAllegations())
    this.updatePreviousAllegations();
  }
  onPreviousAllegationDelete(element,index) {
    
    (this.form.controls['personnelPreviousAllegations'] as FormArray).removeAt(index);
     this.updatePreviousAllegations();
    
  }      
  
    newcaseDetail(model: PersonnelCaseDetails = {} as PersonnelCaseDetails): import("@angular/forms").AbstractControl {
    var caseDetail = this.formBuilder.group({
      id: model.id ? model.id : 0,
      personnelId: model.personnelId ? model.personnelId : 0,
       
      caseDate:model.caseDate?model.caseDate:new Date(),
      caseNumber:model.caseNumber?model.caseNumber:[''],
      title:model.title?model.title:['', Validators.required], 
      caseSection: model.caseSection?model.caseSection:[''],
      currentStatus: model.currentStatus?model.currentStatus:'',      
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return caseDetail;
  }
  updateCaseDetailList() {
    return this.caseDetailDataSource.next(this.getcaseDetailList());
  }
  getcaseDetailList() {
    return (this.form.get('personnelCaseDetails') as FormArray).controls
  }
  onAddCaseDetail() {
    const profileChildren = this.form.get('personnelCaseDetails') as FormArray
    profileChildren.push(this.newcaseDetail())
    this.updateCaseDetailList();
  }
  onCaseDetailDelete(element,index) {
    
    (this.form.controls['personnelCaseDetails'] as FormArray).removeAt(index);
    this.updateCaseDetailList()
    
  }
  newPersonnelChildrens(model: PersonnelChildrens = {} as PersonnelChildrens): import("@angular/forms").AbstractControl {
    var profileChildren = this.formBuilder.group({
      id: model.id ? model.id : 0,
      personnelId: model.personnelId ? model.personnelId : 0,
      firstName: model.firstName ? model.firstName : ['', Validators.required],
      lastName: model.lastName ? model.lastName : '',
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })
    return profileChildren;
  }
  updatePersonnelChildrenList() {
    return this.personnelChildrenDataSource.next(this.getPersonnelChildrenList());
  }
  getPersonnelChildrenList() {
    return (this.form.get('personnelChildrens') as FormArray).controls
  }
  onPersonnelChildrenDelete(element,index) {
   
    (this.form.controls['personnelChildrens'] as FormArray).removeAt(index);
    this.updatePersonnelChildrenList()
  }
  onAddPersonnelChildren() {
    const profileChildren = this.form.get('personnelChildrens') as FormArray
    profileChildren.push(this.newPersonnelChildrens())
    this.updatePersonnelChildrenList();
  }
  newPersonnelSpouse(model: PersonnelSpouses = {} as PersonnelSpouses): import("@angular/forms").AbstractControl {
    var personnelSpouse = this.formBuilder.group({
      id: model.id ? model.id : 0,
      personnelId: model.personnelId ? model.personnelId : 0,
      firstName: model.firstName ? model.firstName : ['', Validators.required],
      lastName: model.lastName ? model.lastName:'',
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return personnelSpouse;
  }
  updateSpouseList() {
    this.personnelSpouseDataSource.next(this.getPersonnelSpouseList());
  }
  getPersonnelSpouseList() {
    return (this.form.get('personnelSpouses') as FormArray).controls
  }
  onPersonnelSpouseDelete(element,index) {
    
    (this.form.controls['personnelSpouses'] as FormArray).removeAt(index);
    this.updateSpouseList()
  }

  onAddPersonnelSpouse() {
    const profileAlias = this.form.get('personnelSpouses') as FormArray
    profileAlias.push(this.newPersonnelSpouse())
    this.updateSpouseList();
  }

 
  saveChanges() {
    if (this.form.valid) {
      this.onSaveChanges.emit(this.form.value)
    }
   
  }

  associateAutoSearch() {

  }

  
 
 
  
  comparePSObjects(object1: any, object2: any) {
    return object1 && object2 && object1.stationId == object2.stationId;
}
onCancel(){
  this.router.navigate(['auth/personnel/personnel-list']);
}

}
