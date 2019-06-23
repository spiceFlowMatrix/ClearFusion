import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PensionDebitAccountComponent } from './pension-debit-account.component';

describe('PensionDebitAccountComponent', () => {
  let component: PensionDebitAccountComponent;
  let fixture: ComponentFixture<PensionDebitAccountComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PensionDebitAccountComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PensionDebitAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
