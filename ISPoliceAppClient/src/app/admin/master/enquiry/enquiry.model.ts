
export interface EnquiryList{
    id: number;
    title: string;
    createdDate: Date; 
    participant: string;
    mom: string;
    outCome: string;
    allegationId: number; 
    allegationEnquiryDocuments: AllegationEnquiryDocument[];
    
}


export interface AllegationEnquiryDocument{
    id: number;
    title: string;
    createdDate: Date; 
    documentUrl: string;
    allegationEnquiryId: number;     
    isActive: boolean | null;
}


