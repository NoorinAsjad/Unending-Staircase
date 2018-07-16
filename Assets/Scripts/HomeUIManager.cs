using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class HomeUIManager : MonoBehaviour {
	string filename = "ResearchSubjects.txt";//should we add path??
	string subjectID;
	string Name;
	string expOrder;
	bool rightOrder = false;
	bool subjectIdEntered = false;
	bool ready = false;
	bool slats = true;
	bool up = true;
	public Text errorText;

	string[] myOrder = new string[] { "r-rs-v-vs", "r-rs-vs-v", "r-v-rs-vs", "r-v-vs-rs", "r-vs-rs-v", "r-vs-v-rs",
		"v-r-vs-rs", "v-vs-r-rs", "v-vs-rs-r", "v-rs-vs-r", "v-r-rs-vs", "v-rs-r-vs",
		"vs-r-v-rs", "vs-v-r-rs", "vs-v-rs-r", "vs-rs-v-r", "vs-rs-r-v", "vs-r-rs-v",
		"rs-vs-v-r", "rs-v-vs-r", "rs-vs-r-v", "rs-v-r-vs", "rs-r-vs-v", "rs-r-v-vs"};


	public void checkexpOrder(string text) {
		int i = 0;
		bool doesntMatch = true;
		while (i < myOrder.Length && doesntMatch) {
			if (text != myOrder [i]) {
				++i;
			} else {
				doesntMatch = false;
			}
		}
		if (doesntMatch) {
			errorText.text = "Wrong combination";
			Invoke ("cancelText", 1f);
		} else {
			expOrder = text;
			rightOrder = true;
		}

	}

	public void withSlats(bool slatsSelected){
		slats = slatsSelected;
	}

	public void goingUp(bool uppity){
		up = uppity;
	}

	public void nameEntered(string text){
		Name = text;
	}

	public void CheckIdEntered(string text) {
		if (text.Length != 3) {
			errorText.text = "Wrong ID";
			Invoke ("cancelText", 1f);
		} else {
			subjectID = text;
			subjectIdEntered = true;
		}

	}

	public void UpdateFile() {
		string localDate = System.DateTime.Now.ToString ("g");
		if (rightOrder && subjectIdEntered) {
			string[] delimiter = new string[]{ "-" };
			ProjectManager.instance.fileName = subjectID.ToString();
			ProjectManager.instance.experimentOrder = expOrder.ToString ().Split(delimiter, System.StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < ProjectManager.instance.experimentOrder.Length; i++) {
				Debug.Log (ProjectManager.instance.experimentOrder [i]);
			}
			string SceneToLoad = ProjectManager.instance.experimentOrder [0];
			using (StreamWriter sw = File.AppendText (filename)) {
				sw.Write (subjectID+",");
				sw.Write (Name+",");
				sw.Write (localDate.ToString()+",");
				sw.Write ("With Slats: "+slats.ToString()+",");
				sw.Write ("Going Up: "+up.ToString()+",");
				sw.Write (expOrder+",");
				sw.Write ("Exp  "+ SceneToLoad+"- ");
			}
			errorText.text = "Generated files";
			ready = true;
		} else {
			errorText.text = "Unable to generate files";
			Invoke ("cancelText", 1f);
		}
	}

	public void startProject() {
		if (ready) {
			bool tempShoes = false;
			ProjectManager.instance.expNumber = 0;
			string SceneToLoad = ProjectManager.instance.experimentOrder [0];
			if (SceneToLoad.Length == 2) {
				tempShoes = true;
			} 
			if (SceneToLoad [0] == 'r') { 
				if (tempShoes) {
					SceneManager.LoadScene ("rickIsSecondAuthor");
				} else {
					SceneManager.LoadScene ("rickIsSecondAuthor 1");
				}
			} else if (SceneToLoad [0] == 'v') {
				if (tempShoes) {
					SceneManager.LoadScene ("vastFinal");
				} else {
					SceneManager.LoadScene ("vastFinal 1");	
				}
			} else {
				errorText.text = "Not Ready";
				Invoke ("cancelText", 1f);
			}
		}
	}

	void cancelText(){
		errorText.text = "";
	}


		
}
