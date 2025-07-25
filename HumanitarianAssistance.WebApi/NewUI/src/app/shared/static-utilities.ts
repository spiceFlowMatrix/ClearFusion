import * as jsPDF from 'jspdf';

export class StaticUtilities {
  static pdfTextCenter(doc: jsPDF, text: string, fontsize: number) {
    const pageWidth = doc.internal.pageSize.width;
    const txtWidth =
      (doc.getStringUnitWidth(text) * fontsize) / doc.internal.scaleFactor;
    return (pageWidth - txtWidth) / 2;
  }
  static pdfTextRight(doc: jsPDF, text: string, fontsize: number) {
    const pageWidth = doc.internal.pageSize.width;
    const txtWidth =
      (doc.getStringUnitWidth(text) * fontsize) / doc.internal.scaleFactor;
    return pageWidth - txtWidth - 10;
  }

  //#region "setLocalDate"
  static setLocalDate(date: any) {
    if (date === null || date === undefined) {
      return null;
    } else {
      return new Date(
        new Date(date).getTime() - new Date().getTimezoneOffset() * 60000
      );
    }
  }
  //#endregion

  //#region "getLocalDate"
  static getLocalDate(date: any) {
    return new Date(
      new Date(date).getFullYear(),
      new Date(date).getMonth(),
      new Date(date).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds(),
      new Date().getMilliseconds()
    );
  }

  static groupBy(list, keyGetter) {
    let newmap = new Map();
    list.forEach((item) => {
         const key = keyGetter(item);
         const collection = newmap.get(key);
         if (!collection) {
          newmap.set(key, [item]);
         } else {
             collection.push(item);
         }
    });
    newmap = new Map(Array.from(newmap).sort((a, b) => a[0] > b[0] ? 1 : -1));
    return newmap;
  }
  //#endregion

  static getDaysInMonth = function(month, year) {
    // Here January is 1 based
    //Day 0 is the last day in the previous month
   return new Date(year, month, 0).getDate();
  // Here January is 0 based
  // return new Date(year, month+1, 0).getDate();
  };

  convert12hTimeToHours(time12h): number {
    const [time, modifier] = time12h.split(' ');
    let [hours, minutes] = time.split(':');
    if (hours === '12') {
      hours = '00';
    }
    if (modifier === 'PM') {
      hours = parseInt(hours, 10) + 12;
    }
    return hours;
  }

  convert12hTimeToMinutes(time12h): number {
    const [time, modifier] = time12h.split(' ');
    let [hours, minutes] = time.split(':');
    if (hours === '12') {
      hours = '00';
    }
    if (modifier === 'PM') {
      hours = parseInt(hours, 10) + 12;
    }
    return minutes;
  }
}
