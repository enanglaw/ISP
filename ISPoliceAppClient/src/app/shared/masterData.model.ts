export interface ZoneDropdownDTO extends ZoneDropdown {
    
    district: DistrictDropdownDTO[];
}
export interface ZoneDropdown {
    zoneId: number;
    zone: string;
    
}



export interface DistrictDropdownDTO extends DistrictDropdown {
    station: StationDropdownDTO[];
}

export class DistrictDropdown{
    districtId: number;
    district: string;
}

export interface StationDropdownDTO {
    stationid: number;
    stationName: string;
}

export interface ControlRoomDSRModel {
    controlRoomId: number;
    entryDate: string;
    date: string;
    pSName: number;
    givenBy: string;
    takenBy: string;
    subject: string;
    caseNo: string;
    complainant: string;
    complainantAddress: string;
}