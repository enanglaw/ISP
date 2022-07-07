export interface NotesList{
    id: number;
    title: string;
    createdDate: Date; 
    description: string;
    allegationId: number; 
   
}


export interface MemorandumList{
    id: number;
    title: string; 
    description: string;
    allegationId: number;     
    isActive: boolean | null;
}