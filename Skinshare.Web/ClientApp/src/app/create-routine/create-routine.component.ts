import {Component, OnInit} from '@angular/core';
import {Location} from "@angular/common";
import {Routine, RoutinesClient} from "../app.generated";

@Component({
  selector: 'app-create-routine',
  templateUrl: './create-routine.component.html',
  styleUrls: ['./create-routine.component.css']
})
export class CreateRoutineComponent implements OnInit {
  public routine: Routine;

  constructor(private routinesClient: RoutinesClient, private location: Location) {
  }

  ngOnInit() {
    console.log(this.location.path(false));
    this.routinesClient.getRoutine('d3c634d2').subscribe(routine => {
      this.routine = routine;
      console.log(this.routine);
    })
  }

}
