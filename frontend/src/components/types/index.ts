export interface Command {
    name: string;
    value: number;
  }
  
  export interface Step {
    name: string;
    startCommands: Command[];
    endCommands: Command[];
  }
  
  export interface Sequence {
    name: string;
    steps: Step[];
  }
  
  export interface Matrix {
    name: string;
    id: string;
  }
  
  export interface Profile {
    id: string;
    name: string;
    matrices: Matrix[];
    sequences: Sequence[];
  }
  