import { MyFile } from './file';

export class Folder {
    constructor(public Id: number = 0,
                public Name: string = '',
                public UserId: string = '',
                public Path: string = '',
                public Files: MyFile[] = [],
                public Folders: Folder[] = [],
                public ParentFolderId?: number,
                public ParentFolder?: Folder) {}
}
