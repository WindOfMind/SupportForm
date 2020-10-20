import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SupportData } from '../models/support-data';
import { RequestType } from '../models/request-type';
import { SupportService } from '../data-services/support-service';
import { catchError, tap } from 'rxjs/operators';

@Component({
  selector: 'app-support-form',
  templateUrl: './support-form.component.html',
  styleUrls: ['./support-form.component.scss']
})
export class SupportFormComponent implements OnInit {
  requestTypes: RequestType[] = [
    RequestType.Claim,
    RequestType.Proposal,
     RequestType.Question
  ];
  isSent: boolean = false;
  supportForm: FormGroup;
  error: string;
  isSubmitting: boolean = false;

  constructor(private formBuilder: FormBuilder, private supportService: SupportService) {
    this.supportForm = this.formBuilder.group({});
   }

  ngOnInit(): void {
    this.supportForm = this.formBuilder.group({
      email : new FormControl('', [Validators.required, Validators.email]),
      phone : new FormControl('', [Validators.required, Validators.pattern("^[0-9]*$"), Validators.minLength(7), Validators.maxLength(8)]),
      userId : new FormControl('', [Validators.maxLength(100)]),
      requestType : new FormControl('', [Validators.required]),
      message : new FormControl('', [Validators.required, Validators.minLength(1)]),
      terms : new FormControl('', [Validators.requiredTrue])
    });
  }

  get email() {
    return this.supportForm.get('email');
  }

  get phone() {
    return this.supportForm.get('phone');
  }

  get userId() {
    return this.supportForm.get('userId');
  }

  get requestType() {
    return this.supportForm.get('requestType');
  }

  get message() {
    return this.supportForm.get('message');
  }

  get terms() {
    return this.supportForm.get('terms');
  }

  onSubmit(supportData: SupportData): void {
    this.isSent = false;
    this.isSubmitting = true;
    this.error = null;

    if (this.supportForm.valid) {
      this.supportService
        .postSupportMessage(supportData)
        .subscribe(
          () => {
            this.isSent = true;
            setTimeout(() => this.isSent = false);
            this.supportForm.reset();
            this.isSubmitting = false;
          },
          error => {
            this.error = error;
            this.isSubmitting = false;
          }
        );

    }
  }
}