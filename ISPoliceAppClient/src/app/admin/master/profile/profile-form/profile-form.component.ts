import { HttpEventType } from '@angular/common/http';
import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { CaseDetail, ProfileAbstract, ProfileAlias, ProfileAssociates, ProfileChildren, ProfileList, ProfileSiblings, ProfileSpouse } from '../profile.model';
import { ProfileService } from '../profile.service';
import { startWith, debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { NewAssociateComponent } from './new-associate/new-associate.component';
import { Router } from '@angular/router';
@Component({
  selector: 'app-profile-form',
  templateUrl: './profile-form.component.html',
  styleUrls: ['./profile-form.component.scss']
})
export class ProfileFormComponent implements OnInit {

  @Input() model: ProfileList;
  @Input() associateListModel: ProfileAssociates[]=[];

  @Input() heading: any;
  @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();

  isFormLoded: boolean = false;
  showLoading: boolean = false;
  form: FormGroup;
  progress = 0;
  // private sub: Subscription;
  statusList = [{ statusId: 1, statusName: "Present" }, { statusId: 2, statusName: "InActive" },
   { statusId: 3, statusName: "Active" }, { statusId: 4, statusName: "OV" }, { statusId: 5, statusName: "Jail" },
    { statusId: 6, statusName: "Absent" }]
  categoryList = [{ Id: 1, Name: "A+" }, { Id: 2, Name: "A" }, { Id: 2, Name: "B" }, { Id: 3, Name: "C" }]
  martialStatusList = [{ Id: 1, Name: "Single" }, { Id: 2, Name: "Married" }, { Id: 3, Name: "Widowed" }, { Id: 4, Name: "Separated" }, { Id: 5, Name: "Divorced" }]
  cityList = [{ Id: 1, Name: "Chennai" }, { Id: 2, Name: "Coimbatore" }, { Id: 3, Name: "Madurai" }, { Id: 4, Name: "Salem" }]
  cityDownList: any[] = [];
  genderDownList  = [{ Id: 1, Name: "Male" }, { Id: 2, Name: "Female" }]

  stationDownList: any[] = [];
  distDropDownList: any[] = [];
  distCityList = [{ Id: 0, Name: "District" }, { Id: 1, Name: "Dist. Within City" }, { Id: 2, Name: "City" }]
  imageUrl = environment.ServerUrl+"Resources/images/noavatar.png";
  // DATA Sources

  profileAssociatesDataSource = new BehaviorSubject<AbstractControl[]>([]);
  profileAssociatesDataSourceColumns: string[] = ['No', 'Name', 'actions'];
  profileAliasDataSource = new BehaviorSubject<AbstractControl[]>([]);
  profileAliasDataSourceColumns: string[] = ['No', 'Name', 'actions'];
  profileSpouseDataSource = new BehaviorSubject<AbstractControl[]>([]);
  profileSpouseDataSourceColumns: string[] = ['No', 'Name', 'actions'];
  profileChildrenDataSource = new BehaviorSubject<AbstractControl[]>([]);
  profileChildrenDataSourceColumns: string[] = ['No', 'Name', 'Gender', 'actions'];
  profileSiblingsDataSource = new BehaviorSubject<AbstractControl[]>([]);
  profileSiblingsDataSourceColumns: string[] = ['No', 'Name', 'Relation', 'actions'];
  profileAbstractDataSource = new BehaviorSubject<AbstractControl[]>([]);
  profileAbstractDataSourceColumns: string[] = ['City/District', 'Murder', 'NDPS', 'ChainSnatch', 'HbDay', 'OtherCase', 'Total', 'actions'];
  caseDetailDataSource = new BehaviorSubject<AbstractControl[]>([]);
  caseDetailDataSourceColumns: string[] = ['PS', 'Section', 'IO', 'Goondas', 'Reason', 'actions'];


  // END of DATA Sources


  associateAutoSearchControl = new FormControl();
  associateFilteredOptions: Observable<any[]>;
  AssociatesList: any[] = [];
  SelectedAssociatesList: any[] = []
  showNewAssociate: boolean = false;
  associateForm: FormGroup;
  constructor(private formBuilder: FormBuilder, private profileService: ProfileService,
    private dialog: MatDialog,private router: Router,private ref: ChangeDetectorRef) {

  }


  ngOnInit(): void {
    
    this.getDistrictList();
    this.getCitytList();
    this.getPSList();
    this.inItForm();
    // this.sub = this.form. yourInputCtrl.valueChanges
    // .pipe(
    //   debounceTime(500),
    //   distinctUntilChanged(), 
    //   startWith(''),
    //   switchMap((val) => {
    //     return this.filterData(val || '');
    //   })
    // ).subscribe((filtered) => {
    //   this.dataFiltered = filtered;
    // });
  }

  filter(val: string): Observable<any[]> {
    // call the service which makes the http-request
    return this.profileService.getGetAssociates(val)
      .pipe(
        map(response => response.filter(option => {

          this.AssociatesList = response;
          return response
        }))
      )
  }
  associateSelected(event, item) {
    
    var selectedAssociate = this.AssociatesList.find(a => a.id == item.id);
    var associates = {} as ProfileAssociates
    associates.id = 0
    associates.associatesId = selectedAssociate.id;
    associates.isActive = true;
    associates.profileId = selectedAssociate.id;

    this.SelectedAssociatesList.push(this.AssociatesList.find(a => a.id == item.id))
    this.profileAssociatesDataSource.next([...this.SelectedAssociatesList]);
    const profileAlias = this.form.get('profileAssociates') as FormArray
    profileAlias.push(this.newprofileAssociates(associates))
    //this.UpdateProfileAssociatesList();

  }
  getPSList() {
    this.profileService.getPSList().subscribe(
      (obj) => {
        this.stationDownList.push(...obj);
      },
      (error) => (console.log(error))
    );
  }

  getCitytList() {
    this.profileService.getCityList().subscribe(
      (obj) => {

        this.cityDownList.push(...obj);
      },
      (error) => (console.log(error))
    );
  }

  getDistrictList() {
    this.profileService.getDistrictList().subscribe(
      (obj) => {

        this.distDropDownList.push(...obj);
      },
      (error) => (console.log(error))
    );
  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.profileService.uploadPhoto(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.form.patchValue({
            image: event.body.dbPath
          });
          this.imageUrl = environment.ServerUrl + event.body.dbPath;
        }
      });
  }
  get f() {
    return this.form.controls;
  }

  get t() {
    // return this.form.controls['profileTransaction'].controls;
    return (this.form.get('profileTransaction') as FormGroup).controls;
  }


  getdistcitySelector(i) {
    var list = (this.form.get('profileAbstracts') as FormArray).controls
    return list[i].get('distCity')?.value
  }

  onDistrictChange(event) {

  }

  inItForm() {
    this.form = this.formBuilder.group({
      id: 0,
      associateAutoSearchControl: [''],
      name: ['', { validators: [Validators.required] }],
      image: ["Resources/images/noavatar.png"],
      hs: [''],
      entryDate: [new Date()],
      isActive: [true],
      profileTransaction: this.formBuilder.group({
        id: 0,
        profileId: [0],
        age: ['',{ validators: [Validators.required] }],
        status: [''],
        category: [''],
        permanentAddres: ['',{ validators: [Validators.required] }],
        presentAddress: [''],
        // photo: ['', { validators: [Validators.required] }],
        fatherName: [''],
        motherName: [''],
        martialStatus: [''],
        spouseName: [''],
        education: [''],
        occupation: [''],
        noOfGoondas: [''],
        securityProceeding: [''],
        dateOfInitiation: [''],
        lastAction: [''],
        lastActionDate: [''],
        bail: [''],
        bailDate: [''],
        entryDate: [new Date()],
        isActive: [true],

      }),
      profileAssociates: new FormArray([
        // this.newprofileAssociates()
      ]),
      profileAlias: new FormArray([
        // this.newProfileAlias()
      ]),
      profileSpouses: new FormArray([
        // this.newProfileSpouse()
      ]),
      profileChildrens: new FormArray([
        // this.newProfileChildren()
      ]),
      profileSiblings: new FormArray([
        // this.newProfileSiblings()
      ]),
      profileAbstracts: new FormArray([
        // this.newprofileAbstract()
      ]),
      caseDetails: new FormArray([
        // this.newcaseDetail()
      ])
    });
    
    if (this.model !== undefined) {
      this.imageUrl=environment.ServerUrl+ this.model.image
      this.form.patchValue(this.model);
      
      let profileAlias = (this.form.get('profileAlias') as FormArray);
      profileAlias.clear();
      this.model.profileAlias.filter(a=>a.isActive).forEach(element => {
        profileAlias.push(this.newProfileAlias(element))
      });

      let newprofileAssociates = (this.form.get('profileAssociates') as FormArray);
      newprofileAssociates.clear();
      this.model.profileAssociates.filter(a=>a.isActive).forEach(element => {
        newprofileAssociates.push(this.newprofileAssociates(element))
      });

      let profileSpouses = (this.form.get('profileSpouses') as FormArray);
      profileSpouses.clear();
      this.model.profileSpouses.filter(a=>a.isActive).forEach(element => {
        profileSpouses.push(this.newProfileSpouse(element))
      });

      let newProfileChildren = (this.form.get('profileChildrens') as FormArray);
      newProfileChildren.clear();
      this.model.profileChildrens.filter(a=>a.isActive).forEach(element => {
        newProfileChildren.push(this.newProfileChildren(element))
      });

      let newProfileSiblings = (this.form.get('profileSiblings') as FormArray);
      newProfileSiblings.clear();
      this.model.profileSiblings.filter(a=>a.isActive).forEach(element => {
        newProfileSiblings.push(this.newProfileSiblings(element))
      });

      let newprofileAbstract = (this.form.get('profileAbstracts') as FormArray);
      newprofileAbstract.clear();
      this.model.profileAbstracts.filter(a=>a.isActive).forEach(element => {
        newprofileAbstract.push(this.newprofileAbstract(element))
      });

      let newcaseDetail = (this.form.get('caseDetails') as FormArray);
      newcaseDetail.clear();
      this.model.caseDetails.filter(a=>a.isActive).forEach(element => {
        newcaseDetail.push(this.newcaseDetail(element))
      });

      
    }
    if(this.associateListModel!== undefined){
     
      this.SelectedAssociatesList=this.associateListModel.map((profile:any)=>{
        return profile.associatesProfileDetail
      }) ;
      this.profileAssociatesDataSource.next([...this.SelectedAssociatesList]);
    }

    /**Checking */

    this.associateFilteredOptions = this.form.get("associateAutoSearchControl")!.valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      distinctUntilChanged(),
      switchMap(val => {
        return this.filter(val)
      })
    )
    // this.form.get('profileAbstracts')?.valueChanges.subscribe(values => {
    //   console.log(values);
    //   debugger;
    //   var formArrey= <FormArray>this.form.controls['profileAbstracts'] ;
    //   values.forEach((element,index) => {
    //     var total=element.murder+element.attmptMurder+element.ndps+element.robbery+element.chainSnatch+
    //     element.mobileSnatch+element.hbDay+element.hbNight+element.otherCase+element.techCase ;
        
    //     formArrey.controls[index].patchValue({
    //       totalCase: total
    //     });
    //     this.ref.detectChanges()
    //   });
    // });
    
    this.updateProfileAbstractList();
    this.updateCaseDetailList();
    this.updateProfileSiblingList();
    this.updateProfileChildrenList();
    this.updateSpouseList();
    //this.UpdateProfileAssociatesList();
    this.updateAliasList()
    this.isFormLoded = true;
    

  }
  onAbstractChange(index){
    var formArrey= <FormArray>this.form.controls['profileAbstracts'] 
    var element=formArrey.controls[index].value;
    var total=element.murder+element.attmptMurder+element.ndps+element.robbery+element.chainSnatch+
        element.mobileSnatch+element.hbDay+element.hbNight+element.otherCase+element.techCase ;
        formArrey.controls[index].patchValue({
                 totalCase: total
               });
  }
  newprofileAbstract(model: ProfileAbstract = {} as ProfileAbstract): import("@angular/forms").AbstractControl {
    var profileAbstract = this.formBuilder.group({
      id: model.id ? model.id : 0,
      profileId: model.profileId ? model.profileId : 0,
      distCity: model.distCity ? model.distCity : 0,
      distCityId: model.distCityId ? model.distCityId : 0,

      entryDate: model.entryDate ? model.entryDate : new Date(),
      jurisdiction: model.jurisdiction ? model.jurisdiction : '',
      murder: model.murder ? model.murder : '',
      attmptMurder: model.attmptMurder ? model.attmptMurder : '',
      ndps: model.ndps ? model.ndps : '',
      robbery: model.robbery ? model.robbery : '',
      chainSnatch: model.chainSnatch ? model.chainSnatch : '',
      mobileSnatch: model.mobileSnatch ? model.mobileSnatch : '',
      hbDay: model.hbDay ? model.hbDay : '',
      hbNight: model.hbNight ? model.hbNight : '',
      otherCase: model.otherCase ? model.otherCase : '',
      techCase: model.techCase ? model.techCase : '',
      totalCase: model.totalCase ? model.totalCase : 0,
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return profileAbstract;
  }
  updateProfileAbstractList() {
    this.profileAbstractDataSource.next(this.getProfileAbstractList());
  }

  getProfileAbstractList() {
    return (this.form.get('profileAbstracts') as FormArray).controls
  }

  onProfileAbstractDelete(element,index) {
    // element.patchValue({
    //   isActive: false
    // });
    (this.form.controls['profileAbstracts'] as FormArray).removeAt(index);
    this.updateProfileAbstractList()
    
  }
  onAddProfileSAbstract() {
    const profileAbstracts = this.form.get('profileAbstracts') as FormArray
    profileAbstracts.push(this.newprofileAbstract())
    this.updateProfileAbstractList();
  }

  newcaseDetail(model: CaseDetail = {} as CaseDetail): import("@angular/forms").AbstractControl {
    var caseDetail = this.formBuilder.group({
      id: model.id ? model.id : 0,
      profileId: model.profileId ? model.profileId : 0,
      entryDate: model.entryDate ? model.entryDate : new Date(),
      ps: model.ps ? model.ps : 0,
      cr: model.cr ? model.cr : '',
      section: model.section ? model.section : '',
      head: model.head ? model.head : '',
      io: model.io ? model.io : '',
      court: model.court ? model.court : '',
      goondas: model.goondas ? model.goondas : '',
      stage: model.stage ? model.stage : '',
      reason: model.reason ? model.reason : '',
      dsr: model.dsr ? model.dsr : '',
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return caseDetail;
  }

  updateCaseDetailList() {
    return this.caseDetailDataSource.next(this.getcaseDetailList());
  }

  getcaseDetailList() {
    return (this.form.get('caseDetails') as FormArray).controls
  }
  onAddCaseDetail() {
    const profileChildren = this.form.get('caseDetails') as FormArray
    profileChildren.push(this.newcaseDetail())
    this.updateCaseDetailList();
  }
  onCaseDetailDelete(element,index) {
    
    (this.form.controls['caseDetails'] as FormArray).removeAt(index);
    this.updateCaseDetailList()
    
  }
  newProfileSiblings(model: ProfileSiblings = {} as ProfileSiblings): import("@angular/forms").AbstractControl {
    var profileSibling = this.formBuilder.group({
      id: model.id ? model.id : 0,
      profileId: model.profileId ? model.profileId : 0,
      name: model.name ? model.name : '',
      // gender: model.gender ? model.gender : '',
      relation: model.relation ? model.relation : '',
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })
    return profileSibling;
  }

  updateProfileSiblingList() {
    return this.profileSiblingsDataSource.next(this.getProfileSiblingList());
  }

  getProfileSiblingList() {
    return (this.form.get('profileSiblings') as FormArray).controls
  }

  onProfileSiblingDelete(element,index) {
    // element.patchValue({
    //   isActive: false
    // });
    (this.form.controls['profileSiblings'] as FormArray).removeAt(index);
    this.updateProfileSiblingList()
  }

  onAddProfileSibling() {
    const profileSiblings = this.form.get('profileSiblings') as FormArray
    profileSiblings.push(this.newProfileSiblings())
    this.updateProfileSiblingList();
  }

  newProfileChildren(model: ProfileChildren = {} as ProfileChildren): import("@angular/forms").AbstractControl {
    var profileChildren = this.formBuilder.group({
      id: model.id ? model.id : 0,
      profileId: model.profileId ? model.profileId : 0,
      name: model.name ? model.name : '',
      gender: model.gender ? model.gender : 0,
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })
    return profileChildren;
  }

  updateProfileChildrenList() {
    return this.profileChildrenDataSource.next(this.getProfileChildrenList());
  }

  getProfileChildrenList() {
    return (this.form.get('profileChildrens') as FormArray).controls
  }

  onProfileChildrenDelete(element,index) {
    // element.patchValue({
    //   isActive: false
    // });
    (this.form.controls['profileChildrens'] as FormArray).removeAt(index);
    this.updateProfileChildrenList()
  }

  onAddProfileChildren() {
    const profileChildren = this.form.get('profileChildrens') as FormArray
    profileChildren.push(this.newProfileChildren())
    this.updateProfileChildrenList();
  }

  newProfileSpouse(model: ProfileSpouse = {} as ProfileSpouse): import("@angular/forms").AbstractControl {
    var profileSpouse = this.formBuilder.group({
      id: model.id ? model.id : 0,
      profileId: model.profileId ? model.profileId : 0,
      name: model.name ? model.name : '',
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return profileSpouse;
  }

  updateSpouseList() {
    this.profileSpouseDataSource.next(this.getProfileSpouseList());
  }

  getProfileSpouseList() {
    return (this.form.get('profileSpouses') as FormArray).controls
  }

  onProfileSpouseDelete(element,index) {
    
    (this.form.controls['profileSpouses'] as FormArray).removeAt(index);
    this.updateSpouseList()
  }

  onAddProfileSpouse() {
    const profileAlias = this.form.get('profileSpouses') as FormArray
    profileAlias.push(this.newProfileSpouse())
    this.updateSpouseList();
  }

  newProfileAlias(model: ProfileAlias = {} as ProfileAlias): import("@angular/forms").AbstractControl {
    var profileAlias = this.formBuilder.group({
      id: model.id ? model.id : 0,
      profileId: model.profileId ? model.profileId : 0,
      name: model.name ? model.name : '',
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return profileAlias;
  }

  newprofileAssociates(model: ProfileAssociates = {} as ProfileAssociates): FormGroup {
    var profileAssociates = this.formBuilder.group({
      id: model.id ? model.id : 0,
      profileId: model.profileId ? model.profileId : 0,
      associatesId: model.associatesId ? model.associatesId : 0,
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })
    return profileAssociates;
  }

  UpdateProfileAssociatesList() {
    this.profileAssociatesDataSource.next(this.getProfileAssociatesList());
  }

  getProfileAssociatesList() {
    return (this.form.get('profileAssociates') as FormArray).controls
  }

  onProfileAssociatesDelete(element,index) {
    (this.form.controls['profileAssociates'] as FormArray).removeAt(index);
    this.UpdateProfileAssociatesList();
  }

  onAddProfileAssociates() {
    const profileAlias = this.form.get('profileAssociates') as FormArray
    profileAlias.push(this.newprofileAssociates())
    this.UpdateProfileAssociatesList();
  }

  saveChanges() {
    if (this.form.valid) {
      this.onSaveChanges.emit(this.form.value)
    }
   
  }

  associateAutoSearch() {

  }

  onCancelNewAssociate() {
    this.showNewAssociate = false;
  }

  onNewAssociatebtnClick() {
    
    // this.inItAssociateForm();
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '50%';


    const dialogRef = this.dialog.open(NewAssociateComponent, dialogConfig);
    //this.showNewAssociate=true;
    dialogRef.afterClosed().subscribe(
      data => {
        
        // var selectedAssociate=this.AssociatesList.find(a => a.id == item.id);
        var associates = {} as ProfileAssociates
        associates.id = 0
        associates.associatesId = data.id;
        associates.isActive = true;
        associates.profileId = data.id;

        this.SelectedAssociatesList.push(data);
        // this.AssociatesList.push(data);
        this.profileAssociatesDataSource.next([...this.SelectedAssociatesList]);
        const profileAlias = this.form.get('profileAssociates') as FormArray
        profileAlias.push(this.newprofileAssociates(associates))
        // const profileAlias = this.form.get('profileAssociates') as FormArray
        // profileAlias.push(data.profileAlias)
        // this.UpdateProfileAssociatesList();
        // this.SelectedAssociatesList.push(this.AssociatesList.find(a => a.id == item.id))
        // this.profileAssociatesDataSource.next([...this.SelectedAssociatesList]);
      }
    );
  }

  updateAliasList() {
    this.profileAliasDataSource.next(this.getProfileAliasList());
  }

  getProfileAliasList() {
    return (this.form.get('profileAlias') as FormArray).controls
  }

  onProfileAliasDelete(element,index) {
    // element.patchValue({
    //   isActive: false
    // });
    (this.form.controls['profileAlias'] as FormArray).removeAt(index);
    this.updateAliasList()
  }

  onAddProfileAlias() {
    const profileAlias = this.form.get('profileAlias') as FormArray
    profileAlias.push(this.newProfileAlias())
    this.updateAliasList();
  }
  comparePSObjects(object1: any, object2: any) {
    return object1 && object2 && object1.stationId == object2.stationId;
}
onCancel(){
  this.router.navigate(['auth/profile-list']);
}

}
