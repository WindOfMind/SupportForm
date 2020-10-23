import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { SupportData } from '../models/support-data';
import { RequestType } from '../models/request-type';
import { SupportService } from '../data-services/support-service';

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
  isSent = false;
  supportForm: FormGroup;
  error: string;
  isSubmitting = false;

  constructor(private formBuilder: FormBuilder, private supportService: SupportService) {
    this.supportForm = this.formBuilder.group({});
   }

  ngOnInit(): void {
    this.supportForm = this.formBuilder.group({
      email : new FormControl('', [Validators.required, Validators.email]),
      phone : new FormControl('', [Validators.required, Validators.pattern('^[0-9]*$'), Validators.minLength(7), Validators.maxLength(8)]),
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

  onSubmit(supportData: SupportData, formDirective: FormGroupDirective): void {
    this.isSent = false;
    this.isSubmitting = true;
    this.error = null;

    if (this.supportForm.valid) {
      this.supportService
        .postSupportMessage(supportData)
        .subscribe(
          () => {
            this.isSent = true;
            setTimeout(() => this.isSent = false, 10000);
            this.ClearSupportForm(formDirective);
          },
          error => {
            this.error = error.message;
            this.isSubmitting = false;
          }
        );
    }
  }

  private ClearSupportForm(formDirective: FormGroupDirective): void {
    formDirective.resetForm();
    this.supportForm.reset();
    this.isSubmitting = false;
  }
}
