import {Component, OnChanges, OnDestroy, OnInit, SimpleChanges} from '@angular/core';
import {RoutinesClient} from "../app.generated";
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-create-routine',
  templateUrl: './create-routine.component.html',
  styleUrls: ['./create-routine.component.css']
})
export class CreateRoutineComponent implements OnInit, OnDestroy {
  public routine;
  public routineForm = this.fb.group({
    title: ['', Validators.compose([Validators.required, Validators.minLength(5)])],
    description: [''],
    morningSteps: this.fb.array([
      ['', Validators.required]
    ]),
    eveningSteps: this.fb.array([
      ['', Validators.required]
    ]),
  });

  constructor(private routinesClient: RoutinesClient, private fb: FormBuilder) {
  }

  get title() { return this.routineForm.get('title') }
  get morningSteps() { return this.routineForm.get('morningSteps') as FormArray; }
  get eveningSteps() { return this.routineForm.get('eveningSteps') as FormArray; }

  ngOnInit() {

  }

  ngOnDestroy(): void {
  }

  onSubmit(): void {
    console.log(this.routineForm.value)
  }

  addStep(formName: string) {
    (this[formName] as FormArray).push(this.fb.control('', Validators.required));
  }

  routineDto() {
    return {
      id: 0,
      title: '',
      steps: []
    }
  }
}
