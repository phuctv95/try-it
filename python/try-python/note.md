## virtualenv

`py -3 -m venv env` (Windows) or `python3 -m venv env` (Linux/Mac) or `virtualenv env`: to init the project.

`env/Scripts/activate.bat`: to activate scripts like `pip list` to execute in current local context.

`deactivate`: to back to normal, global environment.

`pip freeze --local > requirements.txt`: to list all local dependencies.

`pip install -r requirements.txt`: to install all dependencies.
