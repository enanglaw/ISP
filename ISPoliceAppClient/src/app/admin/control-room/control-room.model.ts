export interface ControlRoomList{
    pSName: number;
    caseNo: string;
    givenBy: string;
    takenBy: string;
    subject: string;

}
export interface ControlRoomDSR extends ControlRoomList{
    controlRoomId: number;
    ZoneId: number;
    DistrictId: number;
    PSId: number;
    PStation: number;
    date: string;
    time: string;
    entryDate: string;
   
    categoryId: number;
    do: string;
    dr: string;
    drSource: string;
    sOC: string;
    complainant: string;
    complainantAddress: string;
    pL: string;
    pR: string;
    totalAccused: string;
    detail: string;
    pSNote: string;
    status: number;
    // controlRoomCategory: ControlRoomDSRCategory;
    controlRoomDSRAccuseds: ControlRoomDSRAccused[];
}

export interface ControlRoomDSRAccused {
    controlRoomAccusedId: number;
    controlRoomId: number;
    hsNbr: string;
    accusedName: string;
    accusedAddress: string;
    status: string;
    crimeNumber: string;
    sectionNumber: string;
    isActive: boolean | null;
    // controlRoom: ControlRoomDSR;
    controlRoomDSRAccusedDetails: ControlRoomDSRAccusedDetail[];

}

export interface ControlRoomDSRCategory {
    categoryName: number;
    categoryId: number;
    // controlRoomDSRs: ControlRoomDSR[];
}
export interface ControlRoomDSRAccusedDetail {
    id: number;
    dsrAccusedId: number;
    crimeNumber: string;
    sectionNumber: string;
    isActive: boolean | null;
   
}



