import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, AbstractControl, FormArray, FormBuilder, Validators } from '@angular/forms';
import { BehaviorSubject, from } from 'rxjs';
import { DistrictDropdownDTO, StationDropdownDTO } from 'src/app/shared/masterData.model';
import { RichTextEditorConfig } from 'src/app/shared/richtexteditor.config';
import { ControlRoomDSR, ControlRoomDSRAccused, ControlRoomDSRAccusedDetail } from '../control-room.model';
import { ControlRoomService } from '../control-room.service';
import * as prowritingaid from 'src/prowritingaid'
 
@Component({
  selector: 'app-form-control-room',
  templateUrl: './form-control-room.component.html',
  styleUrls: ['./form-control-room.component.scss']
})
export class FormControlRoomComponent implements OnInit {

  @Input() model: ControlRoomDSR;
  @Input() heading: any;


  @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();

  form: FormGroup;
  categoryList: any = [];
  ZoneList: any = [];
  districtList: DistrictDropdownDTO[] = []
  stationList: StationDropdownDTO[] = []
  accusedListDataSource = new BehaviorSubject<AbstractControl[]>([]);
  columnsToDisplay: string[] = ['No', 'HSNbr', 'AccusedName', 'Status', 'Address', 'CrimeNumber', 'actions'];
  accusedsRows: FormArray = this.formBuilder.array([]);
  accusedsDetailsRows: FormArray = this.formBuilder.array([]);
  // RichTextEditor config Start
  tools = RichTextEditorConfig.tools
  size = RichTextEditorConfig.size
  family = RichTextEditorConfig.family
  isformLoad: boolean = false;
  // RichTextEditor config End

  constructor(private formBuilder: FormBuilder, private controlRoomService: ControlRoomService) { }

  ngOnInit(): void {
    this.getCategoryDropdown()

    this.getzoneDropdown();
    this.initForm();
  }
  initGrammerly() {
    prowritingaid.initProWritingAid("richtxt_controlroom_rte-edit-view");
  }
  UpdateDropdowns() {
    this.form.patchValue({
      districtId: this.model.DistrictId,
      zoneId: this.model.ZoneId,
      psId: this.model.PSId


    });
  }
  onDistrictChange(event) {
    // this.form.get('Zone').setValue(event.value);

    var zone = this.ZoneList.find(a => a.district.find(b => b.districtId == event.value))
    if (zone) {
      // this.form.patchValue({
      //   Zone: zone.zoneId
      // });
      //   this.form.get['Zone'].setValue(zone.zoneId, {
      //     onlySelf: true
      //  })
      this.form.controls['zoneId'].setValue(zone.zoneId);
    }

  }
  onPStationChange(event) {
    // Set StationName START
    var station = this.stationList.find(a => a.stationid == event.value)
    if (station) {
      this.form.patchValue({
        psName: station.stationName
      });
    }
    // Set StationName END


    var district = this.districtList.find(a => a.station.find(b => b.stationid == event.value))
    if (district) {
      this.form.patchValue({
        districtId: district.districtId
      });
      var zone = this.ZoneList.find(a => a.district.find(b => b.districtId == district?.districtId))
      if (zone) {
        this.form.patchValue({
          zoneId: zone.zoneId
        });
      }
    }

  }

  getzoneDropdown() {
    this.controlRoomService.getZoneDrop().subscribe(
      (obj) => {
        
        this.ZoneList.push(...obj);

        this.districtList = this.ZoneList.flatMap(a => a.district);

        this.stationList = this.ZoneList.flatMap(a => a.district).flatMap(a => a.station)

        //this.UpdateDropdowns();
      },
      (error) => (console.log(error))
    );
  }

  initForm() {
    this.form = this.formBuilder.group({
      controlRoomId: [0],
      zoneId: ['', { validators: [Validators.required] }],
      districtId: ['', { validators: [Validators.required] }],
      psId: ['', { validators: [Validators.required] }],
      date: ['', { validators: [Validators.required] }],
      time: [''],
      categoryId: ['', { validators: [Validators.required] }],
      psName: ['', { validators: [Validators.required] }],
      givenBy: ['', { validators: [Validators.required] }],
      takenBy: ['', { validators: [Validators.required] }],
      subject: ['', { validators: [Validators.required] }],
      caseNo: [''],
      do: ['', { validators: [Validators.required] }],
      dr: ['', { validators: [Validators.required] }],
      drSource: [''],
      soc: [''],
      complainant: [''],
      complainantAddress: [''],
      pl: [''],
      pr: [''],
      totalAccused: ['1'],
      detail: [''],
      psNote: [''],
      entryDate: [new Date()],
      // status: [''],
      controlRoomDSRAccuseds: new FormArray([
        this.newAccuse()
      ])
    });

    if (this.model !== undefined) {
      this.form.patchValue(this.model);
      let controlRoomDSRAccusedItems = (this.form.get('controlRoomDSRAccuseds') as FormArray)
      controlRoomDSRAccusedItems.clear();
      this.model.controlRoomDSRAccuseds.filter(a=>a.isActive).forEach((element, count) => {
          controlRoomDSRAccusedItems.push(this.newAccuse(element))
          var DSRAccusedDetails = (controlRoomDSRAccusedItems.controls[count].get('controlRoomDSRAccusedDetails') as FormArray)
          DSRAccusedDetails.clear();
          element.controlRoomDSRAccusedDetails.filter(a=>a.isActive).forEach((accusedDetails, index) => {
              DSRAccusedDetails.push(this.initAccuseDetails(accusedDetails))
          });
      });

      // this.form.controls['controlRoomId'].setValue(this.model.controlRoomId)
      // this.form.controls['controlRoomId'].setValue(this.model.controlRoomId)

      // this.form.controls['caseNo'].setValue(this.model.caseNo)
      // this.form.controls['dte'].setValue(this.model.date)
      
    }
    this.accusedListDataSource.next(this.getAccusedList());
    this.isformLoad = true;
    this.initGrammerly();

  }

  get accusedList(): FormArray {
    return this.form.get("controlRoomDSRAccuseds") as FormArray
  }

  getCategoryDropdown() {
    this.controlRoomService.getCategoryDrop().subscribe(
      (obj) => {
        this.categoryList.push(...obj);
      },
      (error) => (console.log(error))
    );
  }

  get f() {
    return this.form.controls;
  }

  saveChanges() {
    
    if (this.form.valid) {
      this.onSaveChanges.emit(this.form.value)
    }
    // this.controlRoomService.saveControlRoom(this.form.value).subscribe(
    //   (obj) => {

    //   },
    //   (error) => (console.log(error))
    // );
  }

  accusedDetailsList(i) {
    return (this.form.get('controlRoomDSRAccuseds') as FormArray).controls[i].get('controlRoomDSRAccusedDetails')
  }

  newAccuse(model: ControlRoomDSRAccused = {} as ControlRoomDSRAccused): FormGroup {
    
    return this.formBuilder.group({
      controlRoomAccusedId: model.controlRoomAccusedId ? model.controlRoomAccusedId : 0,
      controlRoomId: model.controlRoomId ? model.controlRoomId : 0,
      hsNbr: model.hsNbr ? model.hsNbr : '',
      accusedName: model.accusedName ? model.accusedName : '',
      accusedAddress: model.accusedAddress ? model.accusedAddress : '',
      status: model.status ? model.status : '',
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
      // crimeNumber: model.crimeNumber ? model.crimeNumber : '',
      // sectionNumber: model.sectionNumber ? model.sectionNumber : '',
      controlRoomDSRAccusedDetails: new FormArray([
        this.initAccuseDetails()
      ])
    })
  }

  initAccuseDetails(model: ControlRoomDSRAccusedDetail = {} as ControlRoomDSRAccusedDetail): FormGroup {

    return this.formBuilder.group({
      id: model.id ? model.id : 0,
      dsrAccusedId: model.dsrAccusedId ? model.dsrAccusedId : 0,
      crimeNumber: model.crimeNumber ? model.crimeNumber : '',
      sectionNumber: model.sectionNumber ? model.sectionNumber : '',
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true

    });
  }
  onAddAccuseDetails(form) {
    form.controls.controlRoomDSRAccusedDetails.push(this.initAccuseDetails())
  }
  onDeleteAccuseDetails(form, index) {

    form.patchValue({
      isActive: false
    });;
    // (form.get('controlRoomDSRAccusedDetails') as FormArray).controls[index].patchValue({
    //   isActive: 0
    // });
    // form.patchValue({
    //   isActive: 0
    // });
    // form.controls.controlRoomDSRAccusedDetails.removeAt(index);
  }

  onAccusedChnage(event) {
    
    var totalaccusedNo = this.form.get('totalAccused')?.value;
    if (totalaccusedNo > 0) {
      var itemstoAdd = totalaccusedNo - this.accusedList.length;
      if (itemstoAdd > 0) {
        for (let index = 0; index < itemstoAdd; index++) {
          this.accusedList.push(this.newAccuse());
        }
      } else {
        for (let index = 0; index < Math.abs(itemstoAdd); index++) {
          this.accusedList.removeAt(this.accusedList.length - 1)
        }
      }

    }
    this.accusedListDataSource.next(this.getAccusedList());
  }

  getAccusedList() {

    return (this.form.get('controlRoomDSRAccuseds') as FormArray).controls

  }
  getDetailsArrey(form) {
    return form.controls.controlRoomDSRAccusedDetails.controls;
  }

  onAccusedDelete(id, element) {
    element.patchValue({
      isActive: false
    });
    // this.accusedList.removeAt(id);
    this.accusedListDataSource.next(this.getAccusedList());
    this.form.patchValue({
      totalAccused: parseInt(this.form.value.totalAccused) - 1
    });
  }



}
