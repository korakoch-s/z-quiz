export class Tester {
    public id: number;
    public name: string;
    public registedDate: Date;
    public submittedDate: Date;
    public score: number;
    public maxScore: number;
    public rank: number;

    constructor(id?: number, name?: string, registedDate?: Date) {
        this.id = id || 0;
        this.name = name || undefined;
        this.registedDate = registedDate || new Date();
    }
}
