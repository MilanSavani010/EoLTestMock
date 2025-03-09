  export interface Command {
    name: string;
    value: number;
  }
  
  export interface Observe{
    name: string;
    acceptances : Acceptance[]
  }

  export interface Acceptance
  {
    name:string;
    minimum:number;
    maximum:number;
    result:Result;
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
  
  export interface Message{
    name: string;
    signals: Signal[];
  }

  export interface Signal
  {
    name: string;
    parent: Message;
  }


  export interface Profile {
    id: string;
    name: string;
    matrices: Matrix[];
    sequences: Sequence[];
  }
  
  export enum Result
  {
    UNKNOWN="UNKNOWN",
    ERROR="ERROR",
    OK="OK",
    WARNING="WARNING"
  }