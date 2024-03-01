import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ClassService } from '../../dashboard/class.service';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-score-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './score-form.component.html',
  styleUrl: './score-form.component.css',
})
export class ScoreFormComponent implements OnInit {
  title?: string;
  request?: { score: number | null; classId: number; studentId: number };
  public scoreGroup = new FormGroup({
    score: new FormControl(0, Validators.required),
  });

  constructor(
    public bsModalRef: BsModalRef,
    private classService: ClassService,
    private toast: ToastrService
  ) {}

  ngOnInit(): void {
    this.scoreGroup.setValue({ score: this.request!.score });
  }

  cancel() {
    this.bsModalRef.hide();
  }

  save() {
    if (!this.scoreGroup.invalid) {
      console.log(this.scoreGroup.value);
      const request = {
        score: this.scoreGroup.value.score,
        studentId: this.request?.studentId,
      };

      console.log(request);
      this.classService
        .assignScoreToStudent(this.request!.classId, request)
        .subscribe((res) => {
          this.toast.success('Success', 'Score saved succesfully 👍🏽');
          this.bsModalRef.hide();
        });
    }
  }
}
