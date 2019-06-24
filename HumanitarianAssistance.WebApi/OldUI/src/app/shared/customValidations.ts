import { FormControl } from '@angular/forms';
import 'rxjs/add/observable/timer';

export class CustomValidation {
  static debounceTime = 1000;

  static checkValidDate(control: FormControl) {
    const CurrentDate = new Date().setHours(0, 0, 0, 0);
    const EnteredDate = new Date(control.value).setHours(0, 0, 0, 0);

    if (EnteredDate < CurrentDate) {
      return { checkValidDate: true };
    }
    return null;
  }

  static validAppTime(control: FormControl) {
    if (control.parent !== undefined) {
      const AppointmentDate = control.parent.controls['AppDate'].value;
      if (AppointmentDate !== '' && AppointmentDate !== undefined) {
        const AppointmentTime: Date = control.value;
        const CurrentDate: Date = new Date();
        const AppDateTime = new Date(
          AppointmentDate.setHours(
            AppointmentTime.getHours(),
            AppointmentTime.getMinutes(),
            0,
            0
          )
        );
        if (AppDateTime.getTime() < CurrentDate.getTime()) {
          return { validateAppTime: true };
        }
      }
    }
    return null;
  }

  static validAppDuration(control: FormControl) {
    if (control.parent !== undefined) {
      const AppointmentDate = control.parent.controls['AppDate'].value;
      if (AppointmentDate !== '' && AppointmentDate !== undefined) {
        const AppointmentEnd = control.parent.controls['AppEndTime'].value;
        const AppointmentStart = control.parent.controls['AppStartTime'].value;
        if (AppointmentEnd !== undefined && AppointmentStart !== undefined) {
          if (AppointmentStart.getHours() > AppointmentEnd.getHours()) {
            return { validAppDuration: true };
          } else if (AppointmentStart.getHours() === AppointmentEnd.getHours()) {
            if (AppointmentStart.getMinutes() > AppointmentEnd.getMinutes()) {
              return { validAppDuration: true };
            }
          }
        }
      }
    }
    return null;
  }

  static ValidateEmail(control: FormControl) {
    const EmailRegex: any = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (!EmailRegex.test(control.value)) {
      return { ValidateEmail: true };
    }
    return null;
  }

  static OnlyNumbers(control: FormControl) {
    const NumberRegex: any = /^[0-9]*$/;
    if (!NumberRegex.test(control.value)) {
      return { OnlyNumbers: true };
    }
    return null;
  }

  static DecimalNumber(control: FormControl) {
    // const DecimalRegex: any = /^[0-9]*(\.[0-9]{1,4})?$/;
    const DecimalRegex: any = /^[0-9]*\.?[0-9]*$/;
    if (!DecimalRegex.test(control.value)) {
      return { DecimalNumber: true };
    }
    return null;
  }

  static ComparePassword(control: FormControl) {
    if (control.parent !== undefined) {
      const Password = control.parent.controls['Password'].value;
      const ConfirmPassword = control.parent.controls['ConfirmPassword'].value;
      if (
        Password !== undefined &&
        (ConfirmPassword !== undefined && ConfirmPassword !== '')
      ) {
        if (Password !== ConfirmPassword) {
          return { ComparePassword: true };
        }
      }
      return null;
    }
    return null;
  }

  static ConfirmPassword(control: FormControl) {
    if (control.parent !== undefined) {
      const Password = control.parent.controls['NewPassword'].value;
      const ConfirmPassword = control.parent.controls['ConfirmPassword'].value;
      if (
        Password !== undefined &&
        (ConfirmPassword !== undefined && ConfirmPassword !== '')
      ) {
        if (Password !== ConfirmPassword) {
          return { ComparePassword: true };
        }
      }
      return null;
    }
    return null;
  }

  static checkFormula(control: FormControl) {
    if (control !== undefined && control != null) {
      if (control.value != null) {
        if (control.parent !== undefined) {
          const formulaType = control.parent.controls['isFixed'].value;
          if (formulaType === 'false') {
            if (control.value.indexOf('COST') === -1) {
              return { checkCustomFormula: true };
            }
          } else {
            if (control.value.indexOf('TO') === -1) {
              return { checkFixedFormula: true };
            }
          }
        }
      }
    }
    return null;
  }

  static checkCurrentPasswordForServer(ctrl: FormControl) {
    return (control: FormControl): Promise<any> => {
      return new Promise<any>((resolve, reject) => {
        if (ctrl.value !== undefined) {
          if (ctrl.value === 'abc') {
            resolve({ checkCurrentPasswordForServer: true });
          }
        } else {
          resolve(null);
        }
      });
    };
  }
}
